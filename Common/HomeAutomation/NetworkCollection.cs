using System;
using System.Collections.Generic;
using System.Linq;
using Roomie.Common.HomeAutomation.Exceptions;

namespace Roomie.Common.HomeAutomation
{
    //TODO: make it an ICollection or something?
    public abstract class NetworkCollection : IEnumerable<INetwork>
    {
        private List<INetwork> networks;

        protected NetworkCollection()
        {
            this.networks = new List<INetwork>();
        }

        #region ICollection kind of things
        public void Add(INetwork network)
        {
            networks.Add(network);
        }
        public INetwork this[string name]
        {
            get
            {
                var query = from network in this.networks
                             where network.Name == name
                             select network;

                if (!query.Any())
                {
                    throw new NoMatchingNetworkException(name);
                }
                else if (query.Count() > 1)
                {
                    throw new MultipleMatchingNetworksException(name, query);
                }
                else
                {
                    return query.First();
                }                
            }
        }
        public INetwork this[int index]
        {
            get
            {
                return networks[index];
            }
        }
        public bool Contains(string name)
        {
            foreach (var network in this)
                if (network.Name.Equals(name))
                    return true;
            return false;
        }
        public int Count
        {
            get
            {
                return networks.Count;
            }
        }
        #endregion

        
        private static int LocationCloseness(ILocation location1, string location2)
        {
            return location1.CalculateCloseness(new Location(location2));
        }

        //TODO: fix everythign about this method
        public IDevice GetDevice(string address, INetwork defaultNetwork = null)
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
                    searchSet = AllDevices;
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
                    searchSet = this[networkName].Devices;
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

        public IEnumerable<IDevice> AllDevices
        {
            get
            {
                foreach (var network in this)
                {
                    foreach (var device in network.Devices)
                    {
                        yield return device;
                    }
                }
            }
        }


        #region IEnumerable<Network> implementation
        IEnumerator<INetwork> IEnumerable<INetwork>.GetEnumerator()
        {
            return networks.GetEnumerator();
        }
        #endregion

        #region IEnumerable implementation
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return networks.GetEnumerator();
        }
        #endregion
    }
}
