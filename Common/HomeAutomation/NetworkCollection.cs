using System.Collections.Generic;
using System.Linq;
using Roomie.Common.HomeAutomation.Exceptions;

namespace Roomie.Common.HomeAutomation
{
    //TODO: make it an ICollection or something?
    public abstract class NetworkCollection : IEnumerable<Network>
    {
        private List<Network> networks;

        protected NetworkCollection()
        {
            this.networks = new List<Network>();
        }

        #region ICollection kind of things
        public void Add(Network network)
        {
            networks.Add(network);
        }
        public Network this[string name]
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
        public Network this[int index]
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

        

        public Device GetDevice(string address, Network defaultNetwork = null)
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
            IEnumerable<Device> searchSet;

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

            IEnumerable<Device> results;
            //TODO: select device by ID
            results = from d in searchSet
                        where true
                        //TODO: add networkLocation
                        && ((networkName == null) || d.Network.Name == networkName)
                        && ((networkId == null) || d.Network.Address == networkId)
                        && ((deviceName == null) || (d.Name == deviceName || d.Address == deviceName))
                        && ((locationName == null) || d.Location.Name == locationName)
                        && ((deviceId == null) || d.Address == deviceId)
                        select d;

            var count = results.Count();

            if (count == 0)
            {
                throw new NoMatchingDeviceException(address);
            }
            else if (count > 1)
            {
                throw new MultipleMatchingDevicesException(address, results);
            }
            else
            {
                return results.First();
            }
        }

        public IEnumerable<Device> AllDevices
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
        IEnumerator<Network> IEnumerable<Network>.GetEnumerator()
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
