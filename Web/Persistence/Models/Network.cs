using System;
using System.Collections.Generic;
using System.Linq;
using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Helpers;
using Roomie.Web.Persistence.Repositories;

namespace Roomie.Web.Persistence.Models
{
    public class Network : INetwork, IHasDivId
    {

        public string Address { get; private set; }
        public virtual Computer AttatchedComputer { get; private set; }
        public virtual IEnumerable<Device> Devices { get; private set; }
        public int Id { get; private set; }
        public DateTime? LastPing { get; private set; }
        public string Name { get; private set; }
        public virtual User Owner { get; private set; }

        public bool? IsAvailable
        {
            get
            {
                //TODO: IsConnected (but not by LastPing
                return (AttatchedComputer != null) && (AttatchedComputer.IsConnected);
            }
        }

        public bool IsConnected
        {
            get
            {
                var temp = TimeSinceLastPing;
                if (TimeSinceLastPing == null)
                {
                    return false;
                }

                return temp.Value.TotalSeconds < 10;
            }
        }

        public TimeSpan? TimeSinceLastPing
        {
            get
            {
                if (LastPing == null)
                {
                    return null;
                }

                return DateTime.UtcNow.Subtract(LastPing.Value);
            }
        }

        private Network()
        {
        }

        public Network(string address, Computer attatchedComputer, IEnumerable<Device> devices, int id, DateTime? lastPing, string name, User owner)
        {
            Address = address;
            AttatchedComputer = attatchedComputer;

            if (Devices != null)
            {
                Devices = SortDevices(devices);
            }
            
            Id = id;
            LastPing = lastPing;
            Name = name;
            Owner = owner;
        }

        public static Network Create(string address, User owner, string name, DateTime? lastPing = null, Computer attachedComputer = null, IEnumerable<Device> devices = null)
        {
            var result = new Network
            {
                Address = address,
                AttatchedComputer = attachedComputer,
                Devices = (devices == null) ? null : devices.ToArray(),
                LastPing = lastPing,
                Name = name,
                Owner = owner
            };

            return result;
        }

        public void LoadDevices(IDeviceRepository devices)
        {
            Devices = devices.Get(this);
        }

        public void UpdatePing(Computer computer)
        {
            AttatchedComputer = computer;
            LastPing = DateTime.UtcNow;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void SetId(int id)
        {
            if (Id != 0)
            {
                throw new ArgumentException("Id is already set");
            }

            Id = id;
        }

        private static Device[] SortDevices(IEnumerable<Device> devices)
        {
            var result = devices.ToList();
            result.Sort(new DeviceSort());
            return result.ToArray();
        }

        #region INetworkDevice implementation

        IEnumerable<IDevice> INetwork.Devices
        {
            get
            {
                return Devices;
            }
        }

        #endregion

        #region INetworkDeviceActions implementation

        public IEnumerable<IDeviceActions> DeviceActions
        {
            get
            {
                return Devices;
            }
        }

        #endregion

        #region INetworkState implementation

        IEnumerable<IDeviceState> INetworkState.DeviceStates
        {
            get
            {
                return Devices;
            }
        }

        #endregion

        #region HasId implementation

        public string DivId
        {
            get
            {
                return "network" + Id;
            }
        }

        #endregion
    }
}
