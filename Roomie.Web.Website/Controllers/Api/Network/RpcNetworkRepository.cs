using System.Linq;
using System.Xml.Linq;
using Roomie.Common.Api.Models;
using Roomie.Web.Persistence.Repositories;

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

        public void Update(int id, string name)
        {
            var cache = new InMemoryRepositoryModelCache();
            var network = _networkRepository.Get(_user, id, cache);

            network.UpdateName(name);
            _networkRepository.Update(network);
        }

        public void AddDevice(int id)
        {
            var cache = new InMemoryRepositoryModelCache();
            var network = _networkRepository.Get(_user, id, cache);

            NetworkAction(network, "AddDevice");
        }
        public void RemoveDevice(int id)
        {
            var cache = new InMemoryRepositoryModelCache();
            var network = _networkRepository.Get(_user, id, cache);

            NetworkAction(network, "RemoveDevice");
        }

        public void SyncWholeNetwork(int id, string computerName, string sentDevicesXml)
        {
            var cache = new InMemoryRepositoryModelCache();
            var network = _networkRepository.Get(_user, id, cache);

            var computerRepository = _repositoryFactory.GetComputerRepository();
            var computer = computerRepository.Get(_user, computerName, cache);

            var deviceRepository = _repositoryFactory.GetDeviceRepository();
            var syncWholeNetwork = new Actions.SyncWholeNetwork(computerRepository, deviceRepository, _networkRepository);

            var sentDevices = XElement.Parse("<devices>" + sentDevicesXml + "</devices>").Descendants();

            syncWholeNetwork.Run(
                computer: computer,
                network: network,
                sentDevices: sentDevices,
                user: _user
            );
        }

        public void Delete(int id)
        {
            var cache = new InMemoryRepositoryModelCache();
            var network = _networkRepository.Get(_user, id, cache);

            var deviceRepository = _repositoryFactory.GetDeviceRepository();

            foreach (var device in deviceRepository.Get(network, cache))
            {
                deviceRepository.Remove(device);
            }

            _networkRepository.Remove(network);

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