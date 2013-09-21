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
        public ILocation Location { get; private set; }
        public Network Network { get; private set; }
        public bool? IsConnected { get; private set; }
        public DeviceType Type { get; private set; }
        public IToggleSwitchState ToggleSwitchState { get; private set; }
        public IDimmerSwitchState DimmerSwitchState { get; private set; }
        public IThermostatState ThermostatState { get; private set; }

        private ReadOnlyDeviceState()
        {
        }

        public ReadOnlyDeviceState(string name, string address, ILocation location, Network network, bool? isConnected, DeviceType type, IToggleSwitchState toggleSwitchState, IDimmerSwitchState dimmerSwitchState, IThermostatState thermostatState)
        {
            Name = name;
            Address = address;
            Location = location;
            Network = network;
            IsConnected = isConnected;
            Type = type;
            ToggleSwitchState = toggleSwitchState;
            DimmerSwitchState = dimmerSwitchState;
            ThermostatState = thermostatState;
        }

        //TODO: unit test this
        public static ReadOnlyDeviceState CopyFrom(IDeviceState source)
        {
            var result = new ReadOnlyDeviceState
            {
                Name = source.Name,
                Address = source.Address,
                Location = source.Location.Copy(),
                Network = source.Network,
                IsConnected = source.IsConnected,
                Type = source.Type,
                ToggleSwitchState = (source.ToggleSwitchState == null) ? null : source.ToggleSwitchState.Copy(),
                DimmerSwitchState = (source.DimmerSwitchState == null) ? null : source.DimmerSwitchState.Copy(),
                ThermostatState = (source.ThermostatState == null) ? null : source.ThermostatState.Copy()
            };

            return result;
        }

        public ReadOnlyDeviceState NewWithNetwork(Network network)
        {
            var result = CopyFrom(this);
            result.Network = network;

            return result;
        }

        public ReadOnlyDeviceState NewWithLocation(ILocation location)
        {
            var result = CopyFrom(this);
            result.Location = location.Copy();

            return result;
        }

        //TODO: unit test this
        public static ReadOnlyDeviceState FromXElement(XElement element)
        {
            var name = element.GetAttributeStringValue("Name");
            var notes = element.GetAttributeStringValue("Notes");
            var address = element.GetAttributeStringValue("Address");
            var isConnected = element.GetAttributeBoolValue("IsConnected");
            var type = element.GetAttributeStringValue("Type");
            var locationName = element.GetAttributeStringValue("Location");

            IToggleSwitchState toggleSwitch = null;
            var toggleSwitchElement = element.Element("ToggleSwitch");
            if (toggleSwitchElement != null)
            {
                toggleSwitch = toggleSwitchElement.ToToggleSwitch();
            }

            IDimmerSwitchState dimmerSwitch = null;
            var dimmerSwitchElement = element.Element("DimmerSwitch");
            if (dimmerSwitchElement != null)
            {
                dimmerSwitch = dimmerSwitchElement.ToDimmerSwitch();
            }

            IThermostatState thermostat = null;
            var thermostatElement = element.Element("Thermostat");
            if (thermostatElement != null)
            {
                thermostat = thermostatElement.ToThermostat();
            }

            var result = new ReadOnlyDeviceState
            {
                Name = name,
                Address = address,
                Location = new Location(locationName),
                IsConnected = isConnected,
                Type = DeviceType.GetTypeFromString(type),
                ToggleSwitchState = toggleSwitch,
                DimmerSwitchState = dimmerSwitch,
                ThermostatState = thermostat
            };

            return result;
            
        }

        public void Update(IDeviceState state)
        {
            throw new System.NotImplementedException();
        }
    }
}
