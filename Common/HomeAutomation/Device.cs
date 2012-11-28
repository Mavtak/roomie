using System.Text;
using System.Xml.Linq;

namespace Roomie.Common.HomeAutomation
{
    public abstract class Device : HomeAutomationEntity
    {
        protected Network network { get; set; }
        internal Network Network
        {
            get
            {
                return network;
            }
        }

        protected DeviceLocation location { get; set; }
        internal DeviceLocation Location
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

        public string BuildVirtualAddress(bool justAddress, bool includeDescription)
        {
            string remarks = null;
            if (includeDescription)
            {
                var remarksBuilder = new StringBuilder();

                if (this.Name != null)
                {
                    remarksBuilder.Append("The ");
                    remarksBuilder.Append(this.Name);
                }
                else
                {
                    remarksBuilder.Append("an unnamed device");
                }

                remarksBuilder.Append(" in ");

                if (this.location.IsSet)
                {
                    remarksBuilder.Append("The ");
                    remarksBuilder.Append(this.location.Name);
                }
                else
                {
                    remarksBuilder.Append(" an unknown location");
                }

                remarksBuilder.Append(", connected to ");

                if (this.Network != null)
                {
                    if (this.Network.Name != null)
                    {
                        remarksBuilder.Append("the ");
                        remarksBuilder.Append(this.Network.Name);
                    }
                    else
                    {
                        remarksBuilder.Append("an unnamed network");
                    }
                }
                else
                {
                    remarksBuilder.Append("no network");
                }

                remarks = remarksBuilder.ToString();
            }

            return VirtualAddress.Format(this, justAddress, remarks);
        }

        public XElement ToXElement()
        {
            //TODO: Lock?
            var result = base.ToXElement("HomeAutomationDevice");

            if (Location.IsSet)
                result.Add(new XAttribute("Location", Location.Name));

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
