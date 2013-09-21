using System.Collections.Generic;
using System.Linq;
using Roomie.Common.HomeAutomation.Exceptions;

namespace Roomie.Common.HomeAutomation
{
    public static class NetworkCollectionExtensions
    {
        public static bool Contains(this IEnumerable<INetwork> networks, string name)
        {
            var result = networks.Any(x => x.Name.Equals(name));

            return result;
        }
        public static IEnumerable<IDevice> GetAllDevices(this IEnumerable<INetwork> networks)
        {
            var results = networks.SelectMany(network => network.Devices);

            return results;
        }

        //TODO: fix everythign about this method
        public static IDevice GetDevice(this IEnumerable<INetwork> networks, string address, INetwork defaultNetwork = null)
        {
            //string use network location
            string networkLocation;
            string networkName;
            string networkId;
            string locationName;
            string deviceName;
            string deviceId;

            var success = Utilities.ParseAddress(
                address: address,
                networkLocation: out networkLocation,
                networkName: out networkName,
                networkId: out networkId,
                locationName: out locationName,
                deviceName: out deviceName,
                deviceId: out deviceId
            );

            if (!success)
            {
                throw new InvalidHomeAutomationAddressException(address);
            }


            //TODO: select network by name or ID
            IEnumerable<IDevice> searchSet;

            if (networkName == null)
            {
                if (defaultNetwork == null)
                {
                    searchSet = networks.GetAllDevices();
                }
                else
                {
                    searchSet = defaultNetwork.Devices;
                }
            }
            else
            {
                try
                {
                    searchSet = networks.GetNetwork(networkName).Devices;
                }
                catch (NoMatchingNetworkException exception)
                {
                    //TODO: the console output should show inner exceptions
                    throw new NoMatchingDeviceException(address, exception);
                }
            }


            //TODO: select device by ID
            var results = (from d in searchSet
                           where true
                               //TODO: add networkLocation
                                 && ((networkName == null) || d.Network.Name == networkName)
                                 && ((networkId == null) || d.Network.Address == networkId)
                                 && ((deviceName == null) || (d.Name == deviceName || d.Address == deviceName))
                                 && ((deviceId == null) || d.Address == deviceId)
                           orderby LocationCloseness(d.Location, locationName) ascending
                           select d)
                      .ToList();


            if (!results.Any())
            {
                throw new NoMatchingDeviceException(address);
            }

            var firstResult = results.First();
            results = results.Where(d => LocationCloseness(d.Location, locationName) == LocationCloseness(firstResult.Location, locationName)).ToList();

            if (results.Count() > 1)
            {
                throw new MultipleMatchingDevicesException(address, results);
            }

            return results.First();
        }

        public static T GetNetwork<T>(this IEnumerable<T> networks, string name)
            where T : INetwork
        {
            var query = from network in networks
                        where network.Name == name
                        select network;

            if (!query.Any())
            {
                throw new NoMatchingNetworkException(name);
            }

            if (query.Count() > 1)
            {
                throw new MultipleMatchingNetworksException(name, query.Cast<INetwork>());
            }
            
            return query.First();
        }

        private static int LocationCloseness(ILocation location1, string location2)
        {
            return location1.CalculateCloseness(new Location(location2));
        }
    }
}
