using System.Text;
using System.Xml.Linq;

namespace Roomie.Common.HomeAutomation
{
    public abstract class Device : HomeAutomationEntity
    {
        protected Network network { get; set; }
        internal Network Network_Hack
        {
            get
            {
                return network;
            }
        }

        protected DeviceLocation location { get; set; }
        internal DeviceLocation Location_Hack
        {
            get
            {
                return location;
            }
        }

        public Device(DeviceLocation location, Network network)
        {
            this.location = location;
            this.network = network;
        }

        public DeviceType Type { get; set; }
        public int MaxPower { get; set; }
        public bool? IsConnected { get; set; }
        
        protected int? power { get; set; }
        public abstract int? Power { get; set; }

        public bool IsOff
        {
            get
            {
                return power == 0;
            }
        }

        public bool IsOn
        {
            get
            {
                return !IsOff;
            }
        }

        public int? Percentage
        {
            get
            {
                if (Power == null)
                {
                    return null;
                }
                else if (MaxPower == 0)
                {
                    //assume that the power is a percentage.
                    return (Power <= 100) ? Power : 100;
                }
                else
                {
                    return Power / MaxPower;
                }
            }
        }

        public string FullAddress
        {
            get
            {
                return this.BuildAddress();
            }
        }

        public string BareFullAddress
        {
            get
            {
                return this.BuildAddress(true);
            }
        }

        public string BareFullAddressWithDescription
        {
            get
            {
                var remarks = new StringBuilder();

                if (this.Name != null)
                {
                    remarks.Append("The ");
                    remarks.Append(this.Name);
                }
                else
                {
                    remarks.Append("an unnamed device");
                }

                remarks.Append(" in ");

                if (this.location.IsSet)
                {
                    remarks.Append("The ");
                    remarks.Append(this.location.Name);
                }
                else
                {
                    remarks.Append(" an unknown location");
                }

                remarks.Append(", connected to ");

                if (this.Network_Hack != null)
                {
                    if (this.Network_Hack.Name != null)
                    {
                        remarks.Append("the ");
                        remarks.Append(this.Network_Hack.Name);
                    }
                    else
                    {
                        remarks.Append("an unnamed network");
                    }
                }
                else
                {
                    remarks.Append("no network");
                }

                return this.BuildAddress(true, remarks.ToString());
            }
        }

        public XElement ToXElement()
        {
            //TODO: Lock?
            var result = base.ToXElement("HomeAutomationDevice");

            if (Location_Hack.IsSet)
                result.Add(new XAttribute("Location", Location_Hack.Name));

            result.Add(new XAttribute("Type", Type));
            //TODO: LastPoll

            if (power != null)
                result.Add(new XAttribute("Power", power.Value));

            if (IsConnected != null)
                result.Add(new XAttribute("IsConnected", IsConnected));

            return result;
        }

        public override void FromXElement(XElement element)
        {
            base.FromXElement(element);

            power = element.GetAttributeIntValue("Power");
            IsConnected = element.GetAttributeBoolValue("IsConnected");
            Type = element.GetAttributeStringValue("Type");
            location.Name = element.GetAttributeStringValue("Location");
        }
    }
}
