using System.Xml.Linq;
using Roomie.Common.HomeAutomation.BinarySensors;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.MultilevelSensors;
using Roomie.Common.HomeAutomation.MultilevelSwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.Measurements.Humidity;
using Roomie.Common.Measurements.Illuminance;
using Roomie.Common.Measurements.Power;
using Roomie.Common.Measurements.Temperature;

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
        public IMultilevelSensorState<IPower> PowerSensorState { get; private set; }
        public IMultilevelSensorState<ITemperature> TemperatureSensorState { get; private set; }
        public IMultilevelSensorState<IHumidity> HumiditySensorState { get; private set; }
        public IMultilevelSensorState<IIlluminance> IlluminanceSensorState { get; private set; }
        public IMultilevelSwitchState MultilevelSwitchState { get; private set; }
        public IBinarySensorState BinarySensorState { get; private set; }
        public IThermostatState ThermostatState { get; private set; }
        public IKeypadState KeypadState { get; private set; }

        private ReadOnlyDeviceState()
        {
        }

        public ReadOnlyDeviceState(string name, string address, ILocation location, INetwork network, bool? isConnected, DeviceType type, IBinarySwitchState toggleSwitchState, IMultilevelSwitchState dimmerSwitchState, IBinarySensorState binarySensorState, IMultilevelSensorState<IPower> powerSensorState, IMultilevelSensorState<ITemperature> temperatureSensorState, IMultilevelSensorState<IHumidity> humiditySensorState, IMultilevelSensorState<IIlluminance> illuminanceSensorState, IThermostatState thermostatState, IKeypadState keypadState)
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
            PowerSensorState = powerSensorState;
            TemperatureSensorState = temperatureSensorState;
            HumiditySensorState = humiditySensorState;
            IlluminanceSensorState = illuminanceSensorState;
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
                PowerSensorState = (source.PowerSensorState == null)?null:source.PowerSensorState.Copy(),
                TemperatureSensorState = (source.TemperatureSensorState == null) ? null : source.TemperatureSensorState.Copy(),
                HumiditySensorState = (source.HumiditySensorState == null) ? null : source.HumiditySensorState.Copy(),
                IlluminanceSensorState = (source.IlluminanceSensorState == null) ? null : source.IlluminanceSensorState.Copy(),
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

            ReadOnlyMultilevelSensorState<IPower> powerSensor = null;
            var powerSensorElement = element.Element("PowerSensor");
            if (powerSensorElement != null)
            {
                powerSensor = powerSensorElement.ToMultilevelSensor<IPower>();
            }

            ReadOnlyMultilevelSensorState<ITemperature> temperatureSensor = null;
            var temperatureSensorElement = element.Element("TemperatureSensor");
            if (temperatureSensorElement != null)
            {
                temperatureSensor = temperatureSensorElement.ToMultilevelSensor<ITemperature>();
            }

            ReadOnlyMultilevelSensorState<IHumidity> humiditySensor = null;
            var humiditySensorElement = element.Element("HumiditySensor");
            if (humiditySensorElement != null)
            {
                humiditySensor = humiditySensorElement.ToMultilevelSensor<IHumidity>();
            }

            ReadOnlyMultilevelSensorState<IIlluminance> illuminanceSensor = null;
            var illuminanceSensorElement = element.Element("IlluminanceSensor");
            if (illuminanceSensorElement != null)
            {
                illuminanceSensor = illuminanceSensorElement.ToMultilevelSensor<IIlluminance>();
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
                PowerSensorState = powerSensor,
                TemperatureSensorState = temperatureSensor,
                HumiditySensorState = humiditySensor,
                IlluminanceSensorState = illuminanceSensor,
                ThermostatState = thermostat,
                KeypadState = keypad
            };

            return result;
            
        }
    }
}
