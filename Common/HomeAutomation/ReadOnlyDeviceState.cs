using System.Xml.Linq;
using Roomie.Common.HomeAutomation.DimmerSwitches;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.ToggleSwitches;

namespace Roomie.Common.HomeAutomation
{
    public class ReadOnlyDeviceState : IDeviceState
    {
        public string Name { get; private set; }
        public string Address { get; private set; }
        public ILocation Location { get; private set; }
        public INetworkState NetworkState { get; private set; }
        public bool? IsConnected { get; private set; }
        public DeviceType Type { get; private set; }
        public IBinarySwitchState ToggleSwitchState { get; private set; }
        public IDimmerSwitchState DimmerSwitchState { get; private set; }
        public IThermostatState ThermostatState { get; private set; }
        public IKeypadState KeypadState { get; private set; }

        private ReadOnlyDeviceState()
        {
        }

        public ReadOnlyDeviceState(string name, string address, ILocation location, INetwork network, bool? isConnected, DeviceType type, IBinarySwitchState toggleSwitchState, IDimmerSwitchState dimmerSwitchState, IThermostatState thermostatState, IKeypadState keypadState)
        {
            Name = name;
            Address = address;
            Location = location;
            NetworkState = network;
            IsConnected = isConnected;
            Type = type;
            ToggleSwitchState = toggleSwitchState;
            DimmerSwitchState = dimmerSwitchState;
            ThermostatState = thermostatState;
            KeypadState = keypadState;
        }

        //TODO: unit test this
        public static ReadOnlyDeviceState CopyFrom(IDeviceState source)
        {
            var result = new ReadOnlyDeviceState
            {
                Name = source.Name,
                Address = source.Address,
                Location = source.Location.Copy(),
                NetworkState = source.NetworkState,
                IsConnected = source.IsConnected,
                Type = source.Type,
                ToggleSwitchState = (source.ToggleSwitchState == null) ? null : source.ToggleSwitchState.Copy(),
                DimmerSwitchState = (source.DimmerSwitchState == null) ? null : source.DimmerSwitchState.Copy(),
                ThermostatState = (source.ThermostatState == null) ? null : source.ThermostatState.Copy(),
                KeypadState = (source.KeypadState == null) ? null : source.KeypadState.Copy()
            };

            return result;
        }

        public ReadOnlyDeviceState NewWithNetwork(INetwork network)
        {
            var result = CopyFrom(this);
            result.NetworkState = network;

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

            IBinarySwitchState toggleSwitch = null;
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

            IKeypadState keypad = null;
            var keypadElement = element.Element("Keypad");
            if (keypadElement != null)
            {
                keypad = keypadElement.ToKeypad();
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
                ThermostatState = thermostat,
                KeypadState = keypad
            };

            return result;
            
        }

        public void Update(IDeviceState state)
        {
            throw new System.NotImplementedException();
        }
    }
}
