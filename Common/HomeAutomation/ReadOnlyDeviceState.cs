using System.Xml.Linq;
using Roomie.Common.HomeAutomation.BinarySensors;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.MultilevelSwitches;
using Roomie.Common.HomeAutomation.Thermostats;

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
        public IBinarySwitchState BinarySwitchState { get; private set; }
        public IMultilevelSwitchState MultilevelSwitchState { get; private set; }
        public IBinarySensorState BinarySensorState { get; private set; }
        public IThermostatState ThermostatState { get; private set; }
        public IKeypadState KeypadState { get; private set; }

        private ReadOnlyDeviceState()
        {
        }

        public ReadOnlyDeviceState(string name, string address, ILocation location, INetwork network, bool? isConnected, DeviceType type, IBinarySwitchState toggleSwitchState, IMultilevelSwitchState dimmerSwitchState, IBinarySensorState binarySensorState, IThermostatState thermostatState, IKeypadState keypadState)
        {
            Name = name;
            Address = address;
            Location = location;
            NetworkState = network;
            IsConnected = isConnected;
            Type = type;
            BinarySwitchState = toggleSwitchState;
            MultilevelSwitchState = dimmerSwitchState;
            BinarySensorState = binarySensorState;
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
                BinarySwitchState = (source.BinarySwitchState == null) ? null : source.BinarySwitchState.Copy(),
                MultilevelSwitchState = (source.MultilevelSwitchState == null) ? null : source.MultilevelSwitchState.Copy(),
                BinarySensorState = (source.BinarySensorState == null)?null : source.BinarySensorState.Copy(),
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

            IMultilevelSwitchState dimmerSwitch = null;
            var dimmerSwitchElement = element.Element("DimmerSwitch");
            if (dimmerSwitchElement != null)
            {
                dimmerSwitch = dimmerSwitchElement.ToDimmerSwitch();
            }

            IBinarySensorState binarySensor = null;
            var binarySensorElement = element.Element("BinarySensor");
            if (binarySensorElement != null)
            {
                binarySensor = binarySensorElement.ToBinarySensor();
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
                BinarySwitchState = toggleSwitch,
                MultilevelSwitchState = dimmerSwitch,
                BinarySensorState = binarySensor,
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
