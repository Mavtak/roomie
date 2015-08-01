using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Persistence.Models
{
    [Table("NetworkModels")]
    public class EntityFrameworkNetworkModel : INetwork, IHasDivId
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        
        public virtual EntityFrameworkUserModel Owner { get; set; }
        //public string Address { get; set; }
        //public string Name { get; set; }

        public virtual EntityFrameworkComputerModel AttatchedComputer { get; set; }

        public EntityFrameworkNetworkModel()
        {
            Devices = new List<EntityFrameworkDeviceModel>();

        }

        public EntityFrameworkNetworkModel(string address)
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
        
        
        public virtual ICollection<EntityFrameworkDeviceModel> Devices { get; set; }

        #region Object overrides

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var that = obj as EntityFrameworkNetworkModel;

            if (obj == null)
            {
                return false;
            }

            return this.Equals(that);
        }

        public bool Equals(EntityFrameworkNetworkModel that)
        {
            if (!base.Equals(that))
            {
                return false;
            }

            if (!EntityFrameworkUserModel.Equals(this.Owner, that.Owner))
            {
                return false;
            }

            return true;
        }

        public static bool Equals(EntityFrameworkNetworkModel network1, EntityFrameworkNetworkModel network2)
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