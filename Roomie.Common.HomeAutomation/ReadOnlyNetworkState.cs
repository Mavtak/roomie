using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Roomie.Common.HomeAutomation
{
    public class ReadOnlyNetworkState : INetworkState
    {
        public string Name { get; private set; }
        public string Address { get; private set; }
        public IEnumerable<IDeviceState> DeviceStates { get; private set; }

        private ReadOnlyNetworkState()
        {
        }

        public ReadOnlyNetworkState(string name, string address, IEnumerable<ReadOnlyDeviceState> deviceStates)
        {
            Name = name;
            Address = address;
            DeviceStates = deviceStates.ToList();
        }

        public static ReadOnlyNetworkState FromXElement(XElement element)
        {
            var name = element.GetAttributeStringValue("Name");
            var address = element.GetAttributeStringValue("Address");

            var devices = new List<ReadOnlyDeviceState>();

            foreach (var deviceElement in element.Elements())
            {
                var device = deviceElement.ToDeviceState();
                devices.Add(device);
            }

            var result = new ReadOnlyNetworkState
            {
                Address = address,
                Name = name,
                DeviceStates = devices
            };

            return result;
        }
    }
}
