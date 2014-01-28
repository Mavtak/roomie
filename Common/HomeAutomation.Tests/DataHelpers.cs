using System.Collections.Generic;
using Roomie.Common.HomeAutomation.DimmerSwitches;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.Keypads.Buttons;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Cores;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.HomeAutomation.ToggleSwitches;
using Roomie.Common.Measurements.Temperature;

namespace Roomie.Common.HomeAutomation.Tests
{
    public static class DataHelpers
    {
        private static int _id;
        public static IDeviceState GenerateExampleDevice(DeviceType type, bool includeToggle, bool includeDimmer, bool includeThermostat, bool includeKeypad)
        {
            var toggle = new ReadOnlyToggleSwitchState(ToggleSwitchPower.On);
            var dimmer = new ReadOnlyDimmerSwitchState(25, 100);

            var thermostatCoreModes = new[] { ThermostatMode.Auto, ThermostatMode.Cool, ThermostatMode.Heat, ThermostatMode.FanOnly, ThermostatMode.Off };
            var thermostatCore = new ReadOnlyThermostatCoreState(thermostatCoreModes, ThermostatMode.Cool, ThermostatCurrentAction.Cooling);

            var thermostatFanModes = new[] { ThermostatFanMode.Auto, ThermostatFanMode.On };
            var thermostatFan = new ReadOnlyThermostatFanState(thermostatFanModes, ThermostatFanMode.Auto, ThermostatFanCurrentAction.On);

            var thermostatSetpoints = new ReadOnlyThermostatSetpointCollection(new Dictionary<ThermostatSetpointType, ITemperature>
                {
                    {ThermostatSetpointType.Cool, new FahrenheitTemperature(74)},
                    {ThermostatSetpointType.Heat, new FahrenheitTemperature(70)}
                });

            
            var thermostat = new ReadOnlyThermostatState(new FahrenheitTemperature(75), thermostatCore, thermostatFan, thermostatSetpoints);

            var keypadButtons = new []
                {
                    new ReadOnlyKeypadButtonState("1", false),
                    new ReadOnlyKeypadButtonState("2", true),
                    new ReadOnlyKeypadButtonState("3", null)
                };

            var keypad = new ReadOnlyKeypadState(keypadButtons);

            var location = new Location("Here");

            var address = _id.ToString();
            _id++;

            if (!includeToggle)
            {
                toggle = null;
            }

            if (!includeDimmer)
            {
                dimmer = null;
            }

            if (!includeThermostat)
            {
                thermostat = null;
            }

            if (!includeKeypad)
            {
                keypad = null;
            }

            var device = new ReadOnlyDeviceState("Sample Device", address, location, null, true, type, toggle, dimmer, thermostat, keypad);

            return device;
        }

        public static IEnumerable<IDeviceState> GenerateExampleDevices(int count, bool includeToggle, bool includeDimmer, bool includeThermostat, bool includeKeypad)
        {
            for (var i = 0; i < count; i++)
            {
                DeviceType type;

                switch (i % 8)
                {
                    case 0:
                        type = DeviceType.Controller;
                        break;

                    case 1:
                        type = DeviceType.Dimmable;
                        break;

                    case 2:
                        type = DeviceType.MotionDetector;
                        break;

                    case 3:
                        type = DeviceType.Relay;
                        break;

                    case 4:
                        type = DeviceType.Switch;
                        break;

                    case 5:
                        type = DeviceType.Thermostat;
                        break;

                    case 6:
                        type = DeviceType.Keypad;
                        break;

                    default:
                        type = DeviceType.Unknown;
                        break;
                }

                var device = GenerateExampleDevice(type, includeToggle, includeDimmer, includeThermostat, includeKeypad);

                yield return device;
            }
        }
    }
}
