using System.Xml.Linq;
using Roomie.Common.HomeAutomation.DimmerSwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.ToggleSwitches;

namespace Roomie.Common.HomeAutomation
{
    public class ReadOnlyDeviceState : IDeviceState
    {
        public string Name { get; private set; }
        public string Address { get; private set; }
        public DeviceLocation Location { get; private set; }
        public Network Network { get; private set; }
        public bool? IsConnected { get; private set; }
        public DeviceType Type { get; private set; }
        public IToggleSwitchState ToggleSwitchState { get; private set; }
        public IDimmerSwitchState DimmerSwitchState { get; private set; }
        public IThermostatState ThermostatState { get; private set; }

        //TODO: unit test this
        public static ReadOnlyDeviceState CopyFrom(IDeviceState source)
        {
            var result = new ReadOnlyDeviceState
            {
                Name = source.Name,
                Address = source.Address,
                Location = source.Location,
                Network =  source.Network,
                IsConnected =  source.IsConnected,
                Type = source.Type,
                ToggleSwitchState = source.ToggleSwitchState.Copy(),
                DimmerSwitchState = source.DimmerSwitchState.Copy(),
                ThermostatState = source.ThermostatState.Copy()
            };

            return result;
        }

        public ReadOnlyDeviceState NewWithNetwork(Network network)
        {
            var result = CopyFrom(this);
            result.Network = network;

            return result;
        }

        public ReadOnlyDeviceState NewWithLocation(DeviceLocation location)
        {
            var result = CopyFrom(this);
            result.Location = location;

            return result;
        }

        //TODO: unit test this
        public static ReadOnlyDeviceState FromXElement(XElement element)
        {
            var name = element.GetAttributeStringValue("Name");
            var notes = element.GetAttributeStringValue("Notes");
            var address = element.GetAttributeStringValue("Address");
            var power = element.GetAttributeIntValue("Power");
            var maxPower = element.GetAttributeIntValue("MaxPower");
            var isConnected = element.GetAttributeBoolValue("IsConnected");
            var type = element.GetAttributeStringValue("Type");
            var locationName = element.GetAttributeStringValue("Location");

            ToggleSwitchPower? togglePower = null;

            if (Utilities.IsOn(power))
            {
                togglePower = ToggleSwitchPower.On;
            }
            else if(Utilities.IsOff(power))
            {
                togglePower = ToggleSwitchPower.Off;
            }

            var result = new ReadOnlyDeviceState
            {
                Name = name,
                Address = address,
                Location = new DeviceLocation
                {
                    Name = locationName
                },
                IsConnected = isConnected,
                Type = DeviceType.GetTypeFromString(type),
                ToggleSwitchState = new ReadOnlyToggleSwitchState(togglePower),
                DimmerSwitchState = new ReadOnlyDimmerSwitchState(power, maxPower),
                ThermostatState = new ReadOnlyThermostatState()
            };

            return result;
            
        }
    }
}
