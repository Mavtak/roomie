using System.Xml.Linq;

namespace Roomie.Common.HomeAutomation
{
    public abstract class Network : HomeAutomationEntity, IHasName
    {
        protected DeviceCollection devices { get; set; }
        internal DeviceCollection Devices
        {
            get
            {
                return devices;
            }
        }

        public Network(DeviceCollection devices)
        {
            this.devices = devices;
        }

        public XElement ToXElement()
        {
            var result = base.ToXElement("HomeAutomationNetwork");

            foreach (var device in this.devices)
            {
                result.Add(device.ToXElement());
            }

            return result;
        }
    }
}
