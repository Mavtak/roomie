using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Helpers;
using BaseDevice = Roomie.Common.HomeAutomation.Device;
using BaseLocation = Roomie.Common.HomeAutomation.DeviceLocation;
using BaseNetwork = Roomie.Common.HomeAutomation.Network;

namespace Roomie.Web.Persistence.Models
{
    public class DeviceModel : BaseDevice, IHasDivId
    {
        [Key]
        public int Id { get; set; }

        public virtual NetworkModel Network
        {
            get
            {
                return (NetworkModel)base.network;
            }
            set
            {
                base.network = (BaseNetwork)value;
            }
        }

        public virtual DeviceLocationModel Location
        {
            get
            {
                return (DeviceLocationModel)base.location;
            }
            set
            {
                base.location = (BaseLocation)value;
            }
        }

        public override IToggleSwitch ToggleSwitch
        {
            get
            {
                return new ToggleSwitchModel(this);
            }
        }

        public override IDimmerSwitch DimmerSwitch
        {
            get
            {
                return new DimmerSwitchModel(this);
            }
        }

        public int? Power
        {
            get
            {
                return base.power;
            }
            set
            {
                base.power = value;
            }
        }

        public DeviceModel()
            : base(new DeviceLocationModel(), null)
        { }

        //TODO: improve this.
        public DeviceModel(XElement element)
            : this()
        {
            base.FromXElement(element);
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

        public void UpdatePing()
        {
            LastPing = DateTime.UtcNow;
        }

        #endregion

        public bool IsAvailable
        {
            get
            {
                return (IsConnected == true)
                    && (Network != null)
                    && (Network.IsAvailable == true);
            }
        }

        #region Object overrides

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var that = obj as DeviceModel;

            if (obj == null)
            {
                return false;
            }

            return this.Equals(that);
        }

        public bool Equals(DeviceModel that)
        {
            if (that == null)
            {
                return false;
            }

            if (this.Address != that.Address)
            {
                return false;
            }

            if (!NetworkModel.Equals(this.Network, that.Network))
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        public override string ToString()
        {
            var builder = new System.Text.StringBuilder();

            builder.Append("Device{Name='");
            builder.Append(Name);
            builder.Append("', Address='");
            builder.Append(Address);
            builder.Append("', Location-'");
            if (Location == null)
            {
                builder.Append("(unknown)");
            }
            else
            {
                builder.Append(Location.Name);
            }
            builder.Append("', Type='");
            builder.Append(Type);
            builder.Append(Type.CanControl ? "(Controllable)" : "(Noncontrollable)");
            builder.Append("', Power='");
            builder.Append(Power);
            builder.Append("', Network='");
            builder.Append((Network != null) ? Network.Name : "(none}");
            builder.Append("'}");

            return builder.ToString();
        }

        #endregion

        #region HasId implementation

        public string DivId
        {
            get
            {
                return "device" + Id;
            }
        }

        #endregion
    }
}