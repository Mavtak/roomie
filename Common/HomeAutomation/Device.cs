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
        public abstract IToggleSwitch ToggleSwitch { get; }
        public int MaxPower { get; set; }
        public bool? IsConnected { get; set; }
        
        protected int? power { get; set; }
        public abstract int? Power { get; set; }

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
            string description = null;
            if (includeDescription)
            {
                description = VirtualAddress.FormatNaturalLanguageDescription(this);
            }

            return VirtualAddress.Format(this, justAddress, description);
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
