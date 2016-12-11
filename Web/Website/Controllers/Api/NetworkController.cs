using System.Collections.Generic;
using System.Linq;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api
{
    [ApiRestrictedAccess]
    public class NetworkController : RoomieBaseApiController
    {
        public IEnumerable<Network> Get()
        {
            var networks = Database.Networks.Get(User);

            foreach (var network in networks)
            {
                network.LoadDevices(Database.Devices);
            }

            var result = networks.Select(GetSerializableVersion);

            return result;
        }

        public Network Get(int id)
        {
            var network = this.SelectNetwork(id);
            var result = GetSerializableVersion(network);

            return result;
        }
        
        private static Network GetSerializableVersion(Network network)
        {
            Computer computer = null;

            if (network.AttatchedComputer != null)
            {
                computer = new Computer(
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

            Device[] devices = null;

            if(network.Devices != null)
            {
                devices = network.Devices
                    .Select(x => new Device(
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

            User owner = null;

            if (network.Owner != null)
            {
                owner = new User(
                    alias: network.Owner.Alias,
                    email: null,
                    id: network.Owner.Id,
                    registeredTimestamp: null,
                    secret: null,
                    token: null
                );
            }

            return new Network(
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
