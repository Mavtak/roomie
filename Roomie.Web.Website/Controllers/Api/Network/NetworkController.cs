using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Xml.Linq;
using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api.Network
{
    [ApiRestrictedAccess]
    public class NetworkController : BaseController
    {
        private IDeviceRepository _deviceRepository;
        private INetworkRepository _networkRepository;

        public NetworkController()
        {
            _deviceRepository = RepositoryFactory.GetDeviceRepository();
            _networkRepository = RepositoryFactory.GetNetworkRepository();
        }

        public IEnumerable<Persistence.Models.Network> Get()
        {
            var cache = new InMemoryRepositoryModelCache();
            var networks = _networkRepository.Get(User, cache);

            foreach (var network in networks)
            {
                network.LoadDevices(_deviceRepository);
            }

            var result = networks.Select(GetSerializableVersion);

            return result;
        }

        public Persistence.Models.Network Get(int id)
        {
            var cache = new InMemoryRepositoryModelCache();
            var network = SelectNetwork(id, cache);
            var result = GetSerializableVersion(network);

            return result;
        }

        public void Put(int id, [FromBody] NetworkUpdateModel update)
        {
            update = update ?? new NetworkUpdateModel();

            var cache = new InMemoryRepositoryModelCache();
            var network = SelectNetwork(id, cache);

            network.UpdateName(update.Name);
            _networkRepository.Update(network);
        }

        public void Post(int id, string action, string computerName, string sentDevicesXml)
        {
            var cache = new InMemoryRepositoryModelCache();
            var network = SelectNetwork(id, cache);

            switch (action)
            {
                case "add-device":
                    NetworkAction(network, "AddDevice");
                    break;

                case "remove-device":
                    NetworkAction(network, "RemoveDevice");
                    break;

                case "sync-whole-network":
                    var computerRepository = RepositoryFactory.GetComputerRepository();
                    var computer = computerRepository.Get(User, computerName);
                    var syncWholeNetwork = new Actions.SyncWholeNetwork(computerRepository, _deviceRepository, _networkRepository);
                    var sentDevices = XElement.Parse("<devices>" + sentDevicesXml + "</devices>").Descendants();

                    syncWholeNetwork.Run(
                        computer: computer,
                        network: network,
                        sentDevices: sentDevices,
                        user: User
                    );
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        private void Delete(int id)
        {
            var cache = new InMemoryRepositoryModelCache();
            var network = SelectNetwork(id, cache);

            foreach (var device in _deviceRepository.Get(network))
            {
                _deviceRepository.Remove(device);
            }

            _networkRepository.Remove(network);

        }

        private void NetworkAction(Persistence.Models.Network network, string action)
        {
            AddTask(
                computer: network.AttatchedComputer,
                origin: "RoomieBot",
                scriptText: $"<HomeAutomation.{action} Network=\"{network.Address}\" />\n<HomeAutomation.SyncWithCloud />"
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

            if(network.Devices != null)
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

        private Persistence.Models.Network SelectNetwork(int id, IRepositoryModelCache cache)
        {
            var network = _networkRepository.Get(User, id, cache);

            if (network == null)
            {
                throw new HttpException(404, "Network not found");
            }

            network.LoadDevices(_deviceRepository);

            return network;
        }
    }
}
