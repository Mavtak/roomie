using System.Collections.Generic;
using System.Linq;
using Roomie.Common.HomeAutomation.Exceptions;

namespace Roomie.Common.HomeAutomation
{
    public class DeviceCollection : System.Collections.Generic.IEnumerable<Device>, System.Collections.IEnumerable, ICollection<Device>
    {
        protected List<Device> devices { get; private set; }
        protected Network network { get; private set; }

        public DeviceCollection(Network network)
        {
            this.network = network;
            this.devices = new List<Device>();
        }

        public virtual void Add(Device device)
        {
            if (this.ContainsAddress(device.Address_Hack))
            {
                //TODO: better exception?
                throw new MultipleMatchingDevicesException(device.Address_Hack, new Device[] { this[device.Address_Hack] });
            }

            devices.Add(device);
        }

        public bool ContainsAddress(string key)
        {
            return devices.Any(device => device.Address_Hack == key);
        }
        public bool ContainsName(string key)
        {
            return devices.Any(device => device.Name == key);
        }
        public bool Contains(string key)
        {
            return ContainsAddress(key) || ContainsAddress(key);
        }

        public int Count
        {
            get
            {
                return devices.Count;
            }
        }

        public Device this[string key]
        {
            get
            {
                var result = from device in devices
                             where device.Address_Hack == key
                             || device.Name == key
                             select device;

                var count = result.Count();
                if (count == 0)
                {
                    throw new DeviceNotFoundException(key);
                }
                if (count > 1)
                {
                    throw new MultipleMatchingDevicesException(key, result);
                }
                else
                {
                    return result.First();
                }
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return devices.GetEnumerator();
        }
        System.Collections.Generic.IEnumerator<Device> IEnumerable<Device>.GetEnumerator()
        {
            return devices.GetEnumerator();
        }



        //void ICollection<Device>.Add(Device item)
        //{
            
        //}

        void ICollection<Device>.Clear()
        {
            devices.Clear();
        }

        bool ICollection<Device>.Contains(Device item)
        {
            return devices.Contains(item);
        }

        void ICollection<Device>.CopyTo(Device[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        int ICollection<Device>.Count
        {
            get
            {
                return devices.Count;
            }
        }

        bool ICollection<Device>.IsReadOnly
        {
            get { return false; }
        }

        bool ICollection<Device>.Remove(Device item)
        {
            return devices.Remove(item);
        }
    }
}
