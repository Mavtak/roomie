using System.Collections.Generic;
using System.Linq;
using Roomie.Common.HomeAutomation.Exceptions;

namespace Roomie.Common.HomeAutomation
{
    public class DeviceCollection : ICollection<IDevice>
    {
        protected List<IDevice> devices { get; private set; }
        protected Network network { get; private set; }

        public DeviceCollection(Network network)
        {
            this.network = network;
            this.devices = new List<IDevice>();
        }

        public virtual void Add(IDevice device)
        {
            if (this.ContainsAddress(device.Address))
            {
                //TODO: better exception?
                throw new MultipleMatchingDevicesException(device.Address, new IDevice[] { this[device.Address] });
            }

            devices.Add(device);
        }

        public bool ContainsAddress(string key)
        {
            return devices.Any(device => device.Address == key);
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

        public IDevice this[string key]
        {
            get
            {
                var result = from device in devices
                             where device.Address == key
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
        System.Collections.Generic.IEnumerator<IDevice> IEnumerable<IDevice>.GetEnumerator()
        {
            return devices.GetEnumerator();
        }



        //void ICollection<Device>.Add(Device item)
        //{
            
        //}

        void ICollection<IDevice>.Clear()
        {
            devices.Clear();
        }

        bool ICollection<IDevice>.Contains(IDevice item)
        {
            return devices.Contains(item);
        }

        void ICollection<IDevice>.CopyTo(IDevice[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        int ICollection<IDevice>.Count
        {
            get
            {
                return devices.Count;
            }
        }

        bool ICollection<IDevice>.IsReadOnly
        {
            get { return false; }
        }

        bool ICollection<IDevice>.Remove(IDevice item)
        {
            return devices.Remove(item);
        }
    }
}
