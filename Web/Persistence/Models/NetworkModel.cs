using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Persistence.Models
{
    public class NetworkModel : INetwork, IHasDivId
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        
        public virtual UserModel Owner { get; set; }
        //public string Address { get; set; }
        //public string Name { get; set; }

        public virtual ComputerModel AttatchedComputer { get; set; }

        public NetworkModel()
        {
            Devices = new List<DeviceModel>();

        }

        public NetworkModel(string address)
            : this()
        {
            Address = address;
        }

        #region LastPing

        public DateTime? LastPing { get; set; }

        public TimeSpan? TimeSinceLastPing
        {
            get
            {
                if (LastPing == null)
                    return null;
                return DateTime.UtcNow.Subtract(LastPing.Value);
            }
        }

        public bool IsConnected
        {
            get
            {
                TimeSpan? temp = TimeSinceLastPing;
                if (TimeSinceLastPing == null)
                    return false;

                return temp.Value.TotalSeconds < 10;
            }
        }

        public void UpdatePing()
        {
            LastPing = DateTime.UtcNow;
        }

        #endregion

        public bool? IsAvailable
        {
            get
            { //TODO: IsConnected (but not by LastPing
                return (AttatchedComputer != null)
                    && (AttatchedComputer.IsConnected == true);
            }
        }
        
        
        public virtual ICollection<DeviceModel> Devices { get; set; }

        #region Object overrides

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var that = obj as NetworkModel;

            if (obj == null)
            {
                return false;
            }

            return this.Equals(that);
        }

        public bool Equals(NetworkModel that)
        {
            if (!base.Equals(that))
            {
                return false;
            }

            if (!UserModel.Equals(this.Owner, that.Owner))
            {
                return false;
            }

            return true;
        }

        public static bool Equals(NetworkModel network1, NetworkModel network2)
        {
            if(network1 == null && network2 == null)
            {
                return true;
            }

            if(network1 == null ^ network2 == null)
            {
                return false;
            }

            return network1.Equals(network2);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            var builder = new System.Text.StringBuilder();
            builder.Append("Network{Name=");
            builder.Append(Name);
            builder.Append(", Address='");
            builder.Append(Address);
            builder.Append("', AttatchedComputer=");
            builder.Append(AttatchedComputer);
            builder.Append(", Owner=");
            builder.Append(Owner);
            builder.Append("}");

            return builder.ToString();
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

        #region INetworkDevice

        IEnumerable<IDevice> INetwork.Devices
        {
            get
            {
                return Devices;
            }
        }

        #endregion

        #region INetworkState

        IEnumerable<IDeviceState> INetworkState.DeviceStates
        {
            get
            {
                return Devices;
            }
        }

        #endregion

        #region INetworkDeviceActions

        public IEnumerable<IDeviceActions> DeviceActions
        {
            get
            {
                return Devices;
            }
        }

        #endregion

    }
}