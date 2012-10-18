using System.Collections.Generic;
using BaseNetworkCollection = Roomie.Common.HomeAutomation.NetworkCollection;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public class NetworkCollection : BaseNetworkCollection
    {
        
        public new Network this[string name]
        {
            get
            {
                return (Network)base[name];
            }
        }

        public new Network this[int index]
        {
            get
            {
                return (Network)base[index];
            }
        }

        public Device GetDevice(string address, Network defaultNetwork = null)
        {
            return (Device)base.GetDevice(address, defaultNetwork);
        }

        public new IEnumerable<Device> AllDevices
        {
            get
            {
                foreach (var device in base.AllDevices)
                {
                    yield return (Device)device;
                }
            }
        }

        public Network First()
        {
            foreach (var network in this)
            {
                return (Network)network;
            }

            //TODO: is this the right exception?
            throw new System.IndexOutOfRangeException();
        }

        //TODO: global device addressing.  Format {NetworkName}:{DeviceName}
    }
}
