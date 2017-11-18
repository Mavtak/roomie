using System.Linq;
using System.Xml.Linq;
using Roomie.Common.Api.Models;
using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Website.Controllers.Api.Computer;

namespace Roomie.Web.Website.Controllers.Api.Network
{
    public class RpcNetworkRepository
    {
        private INetworkRepository _networkRepository;
        private IRepositoryFactory _repositoryFactory;
        private Persistence.Models.User _user;

        public RpcNetworkRepository(IRepositoryFactory repositoryFactory, Persistence.Models.User user)
        {
            _repositoryFactory = repositoryFactory;
            _user = user;

            _networkRepository = _repositoryFactory.GetNetworkRepository();
        }

        public Response<Persistence.Models.Network[]> List()
        {
            var cache = new InMemoryRepositoryModelCache();
            var networks = _networkRepository.Get(_user, cache);

            var deviceRepository = _repositoryFactory.GetDeviceRepository();

            foreach (var network in networks)
            {
                network.LoadDevices(deviceRepository);
            }

            var result = networks.Select(GetSerializableVersion)
                .ToArray();

            return Response.Create(result);
        }

        public Response<Persistence.Models.Network> Read(int id)
        {
            var cache = new InMemoryRepositoryModelCache();
            var network = _networkRepository.Get(_user, id, cache);
            var result = GetSerializableVersion(network);

            return Response.Create(result);
        }

        public Response Update(int id, string name)
        {
            var cache = new InMemoryRepositoryModelCache();
            var network = _networkRepository.Get(_user, id, cache);

            if (network == null)
            {
                return RpcNetworkRepositoryHelpers.CreateNotFoundError();
            }

            network.UpdateName(name);
            _networkRepository.Update(network);

            return Response.Empty();
        }

        public Response AddDevice(int id)
        {
            var cache = new InMemoryRepositoryModelCache();
            var network = _networkRepository.Get(_user, id, cache);

            if (network == null)
            {
                return RpcNetworkRepositoryHelpers.CreateNotFoundError();
            }

            NetworkAction(network, "AddDevice");

            return Response.Empty();
        }
        public Response RemoveDevice(int id)
        {
            var cache = new InMemoryRepositoryModelCache();
            var network = _networkRepository.Get(_user, id, cache);

            if (network == null)
            {
                return RpcNetworkRepositoryHelpers.CreateNotFoundError();
            }

            NetworkAction(network, "RemoveDevice");

            return Response.Empty();
        }

        public Response<string[]> SyncWholeNetwork(int id, int computerId, string[] devicesXml)
        {
            var cache = new InMemoryRepositoryModelCache();
            var network = _networkRepository.Get(_user, id, cache);

            if (network == null)
            {
                return RpcNetworkRepositoryHelpers.CreateNotFoundError<string[]>();
            }

            var computerRepository = _repositoryFactory.GetComputerRepository();
            var computer = computerRepository.Get(_user, computerId, cache);

            if (computer == null)
            {
                return RpcComputerRepositoryHelpers.CreateNotFoundError<string[]>();
            }

            return SyncWholeNetwork(network, computer, devicesXml);
        }

        public Response<string[]> SyncWholeNetwork(string networkAddress, string computerName, string[] devicesXml)
        {
            var cache = new InMemoryRepositoryModelCache();
            var network = _networkRepository.Get(_user, networkAddress, cache);

            if (network == null)
            {
                return RpcNetworkRepositoryHelpers.CreateNotFoundError<string[]>();
            }

            var computerRepository = _repositoryFactory.GetComputerRepository();
            var computer = computerRepository.Get(_user, computerName, cache);

            if (computer == null)
            {
                return RpcComputerRepositoryHelpers.CreateNotFoundError<string[]>();
            }

            return SyncWholeNetwork(network, computer, devicesXml);
        }

        private Response<string[]> SyncWholeNetwork(Persistence.Models.Network network, Persistence.Models.Computer computer, string[] devicesXml)
        {
            var computerRepository = _repositoryFactory.GetComputerRepository();
            var deviceRepository = _repositoryFactory.GetDeviceRepository();
            var syncWholeNetwork = new Actions.SyncWholeNetwork(computerRepository, deviceRepository, _networkRepository);

            var sentDevices = devicesXml
                .Select(x => XElement.Parse(x))
                .ToArray();

            var existingDevices = syncWholeNetwork.Run(
                computer: computer,
                network: network,
                sentDevices: sentDevices,
                user: _user
            );

            var existingDevicesXml = existingDevices
                .Select(x => x.ToXElement())
                .Select(x => x.ToString())
                .ToArray();

            return Response.Create(existingDevicesXml);
        }

        public Response Delete(int id)
        {
            var cache = new InMemoryRepositoryModelCache();
            var network = _networkRepository.Get(_user, id, cache);

            if (network == null)
            {
                return RpcNetworkRepositoryHelpers.CreateNotFoundError();
            }

            var deviceRepository = _repositoryFactory.GetDeviceRepository();

            foreach (var device in deviceRepository.Get(network, cache))
            {
                deviceRepository.Remove(device);
            }

            _networkRepository.Remove(network);

            return Response.Empty();
        }

        private void NetworkAction(Persistence.Models.Network network, string action)
        {
            var computerRepository = _repositoryFactory.GetComputerRepository();
            var scriptRepository = _repositoryFactory.GetScriptRepository();
            var taskRepository = _repositoryFactory.GetTaskRepository();
            var runScript = new Computer.Actions.RunScript(computerRepository, scriptRepository, taskRepository);

            runScript.Run(
                computer: network.AttatchedComputer,
                scriptText: $"<HomeAutomation.{action} Network=\"{network.Address}\" />\n<HomeAutomation.SyncWithCloud />",
                source: "RoomieBot",
                updateLastRunScript: false,
                user: _user
            );
        }

        private static Persistence.Models.Network GetSerializableVersion(Persistence.Models.Network network)
        {
            Persistence.Models.Computer computer = null;

            if (network.AttatchedComputer != null)
            {
                computer = new Persistence.Models.Computer(
                    accessKey: null,
                    address: null,
                    encryptionKey: null,
                    id: network.AttatchedComputer.Id,
                    lastPing: network.AttatchedComputer.LastPing,
                    lastScript: null,
                    name: network.AttatchedComputer.Name,
                    owner: null
                );
            }

            Persistence.Models.Device[] devices = null;

            if (network.Devices != null)
            {
                devices = network.Devices
                    .Select(x => new Persistence.Models.Device(
                        address: x.Address,
                        id: x.Id,
                        lastPing: null,
                        name: x.Name,
                        network: null,
                        scripts: null,
                        state: null,
                        tasks: null,
                        type: x.Type
                    ))
                    .ToArray();
            }

            Persistence.Models.User owner = null;

            if (network.Owner != null)
            {
                owner = new Persistence.Models.User(
                    alias: network.Owner.Alias,
                    email: null,
                    id: network.Owner.Id,
                    registeredTimestamp: null,
                    secret: null,
                    token: null
                );
            }

            return new Persistence.Models.Network(
                address: network.Address,
                attatchedComputer: computer,
                devices: devices,
                id: network.Id,
                lastPing: network.LastPing,
                name: network.Name,
                owner: owner
            );
        }
    }
}