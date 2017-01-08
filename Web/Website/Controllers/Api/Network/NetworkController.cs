using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api.Network
{
    [ApiRestrictedAccess]
    public class NetworkController : RoomieBaseApiController
    {
        public IEnumerable<Persistence.Models.Network> Get()
        {
            var networks = Database.Networks.Get(User);

            foreach (var network in networks)
            {
                network.LoadDevices(Database.Devices);
            }

            var result = networks.Select(GetSerializableVersion);

            return result;
        }

        public Persistence.Models.Network Get(int id)
        {
            var network = SelectNetwork(id);
            var result = GetSerializableVersion(network);

            return result;
        }

        public void Put(int id, [FromBody] NetworkUpdateModel update)
        {
            update = update ?? new NetworkUpdateModel();

            var network = SelectNetwork(id);

            network.UpdateName(update.Name);
            Database.Networks.Update(network);
        }

        public void Post(int id, string action)
        {
            var network = SelectNetwork(id);

            switch (action)
            {
                case "add-device":
                    NetworkAction(network, "AddDevice");
                    break;

                case "remove-device":
                    NetworkAction(network, "RemoveDevice");
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        private void Delete(int id)
        {
            var network = SelectNetwork(id);

            foreach (var device in Database.Devices.Get(network))
            {
                Database.Devices.Remove(device);
            }

            Database.Networks.Remove(network);

        }

        private void NetworkAction(Persistence.Models.Network network, string action)
        {
            this.AddTask(
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

        private Persistence.Models.Network SelectNetwork(int id)
        {
            var network = Database.Networks.Get(User, id);

            if (network == null)
            {
                throw new HttpException(404, "Network not found");
            }

            network.LoadDevices(Database.Devices);

            return network;
        }
    }
}
