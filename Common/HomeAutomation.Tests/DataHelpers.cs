using System.Collections.Generic;
using Roomie.Common.HomeAutomation.DimmerSwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.HomeAutomation.ToggleSwitches;
using Roomie.Common.Temperature;

namespace Roomie.Common.HomeAutomation.Tests
{
    public static class DataHelpers
    {
        private static int _id;
        public static IDeviceState GenerateExampleDevice(DeviceType type, bool includeToggle = true, bool includeDimmer = true, bool includeThermostat = true)
        {
            var toggle = new ReadOnlyToggleSwitchState(ToggleSwitchPower.On);
            var dimmer = new ReadOnlyDimmerSwitchState(25, 100);

            var thermostatFanModes = new[] { ThermostatFanMode.Auto, ThermostatFanMode.On };
            var thermostatFan = new ReadOnlyThermostatFanState(thermostatFanModes, ThermostatFanMode.Auto, ThermostatFanCurrentAction.On);

            var thermostatSetpoints = new ReadOnlySetPointCollection(new Dictionary<SetpointType, ITemperature>
                {
                    {SetpointType.Cool, new FahrenheitTemperature(74)},
                    {SetpointType.Heat, new FahrenheitTemperature(70)}
                });

            var thermostatModes = new[] { ThermostatMode.Auto, ThermostatMode.Cool, ThermostatMode.Heat, ThermostatMode.FanOnly, ThermostatMode.Off };
            var thermostat = new ReadOnlyThermostatState(new FahrenheitTemperature(75), thermostatFan, thermostatSetpoints, thermostatModes, ThermostatMode.Cool, ThermostatCurrentAction.Cooling);


            var location = new DeviceLocation
            {
                Name = "Here"
            };

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

            var device = new ReadOnlyDeviceState("Sample Device", address, location, null, true, type, toggle, dimmer, thermostat);

            return device;
        }

        public static IEnumerable<IDeviceState> GenerateExampleDevices(int count, bool includeToggle = true, bool includeDimmer = true, bool includeThermostat = true)
        {
            for (var i = 0; i < count; i++)
            {
                DeviceType type;

                switch (i % 7)
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
                    default:
                        type = DeviceType.Unknown;
                        break;
                }

                var device = GenerateExampleDevice(type, includeToggle, includeDimmer, includeThermostat);

                yield return device;
            }
        }
    }
}
