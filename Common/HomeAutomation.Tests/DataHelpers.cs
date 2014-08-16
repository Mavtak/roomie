using System;
using System.Collections.Generic;
using Roomie.Common.HomeAutomation.BinarySensors;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.Keypads.Buttons;
using Roomie.Common.HomeAutomation.MultilevelSensors;
using Roomie.Common.HomeAutomation.MultilevelSwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Cores;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Measurements.Humidity;
using Roomie.Common.Measurements.Illuminance;
using Roomie.Common.Measurements.Power;
using Roomie.Common.Measurements.Temperature;

namespace Roomie.Common.HomeAutomation.Tests
{
    public static class DataHelpers
    {
        private static int _id;
        public static IDeviceState GenerateExampleDevice(DeviceType type, bool includeCurrentAction, bool includeToggle, bool includeDimmer, bool includeBinarySensor, bool includePowerSensor, bool includeTemperatureSensor, bool includeHumiditySensor, bool includeIlluminanceSensor, bool includeThermostat, bool includeKeypad)
        {
            string currentAction = "Idle";

            var toggle = new ReadOnlyBinarySwitchSwitchState(BinarySwitchPower.On);
            var dimmer = new ReadOnlyMultilevelSwitchState(25, 100);

            var binarySensor = new ReadOnlyBinarySensorState(BinarySensorType.Motion, true, DateTime.UtcNow.AddMinutes(-4));

            var powerSensor = new ReadOnlyMultilevelSensorState<IPower>(new WattsPower(25), DateTime.UtcNow.AddSeconds(-2));

            var temperatureSensor = new ReadOnlyMultilevelSensorState<ITemperature>(new CelsiusTemperature(3), DateTime.UtcNow.AddSeconds(-1));

            var humiditySensor = new ReadOnlyMultilevelSensorState<IHumidity>(new RelativeHumidity(25), DateTime.UtcNow.AddSeconds(-5));

            var illuminanceSensor = new ReadOnlyMultilevelSensorState<IIlluminance>(new LuxIlluminance(50), DateTime.UtcNow.AddSeconds(-4));

            var thermostatCoreModes = new[] { ThermostatMode.Auto, ThermostatMode.Cool, ThermostatMode.Heat, ThermostatMode.FanOnly, ThermostatMode.Off };
            var thermostatCore = new ReadOnlyThermostatCoreState(thermostatCoreModes, ThermostatMode.Cool, ThermostatCurrentAction.Cooling);

            var thermostatFanModes = new[] { ThermostatFanMode.Auto, ThermostatFanMode.On };
            var thermostatFan = new ReadOnlyThermostatFanState(thermostatFanModes, ThermostatFanMode.Auto, ThermostatFanCurrentAction.On);

            var thermostatSetpoints = new ReadOnlyThermostatSetpointCollection(new Dictionary<ThermostatSetpointType, ITemperature>
                {
                    {ThermostatSetpointType.Cool, new FahrenheitTemperature(74)},
                    {ThermostatSetpointType.Heat, new FahrenheitTemperature(70)}
                });

            
            var thermostat = new ReadOnlyThermostatState(thermostatCore, thermostatFan, thermostatSetpoints);

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


            if (!includeCurrentAction)
            {
                currentAction = null;
            }

            if (!includeToggle)
            {
                toggle = null;
            }

            if (!includeDimmer)
            {
                dimmer = null;
            }

            if (!includeBinarySensor)
            {
                binarySensor = null;
            }

            if (!includePowerSensor)
            {
                powerSensor = null;
            }

            if (!includeTemperatureSensor)
            {
                temperatureSensor = null;
            }

            if (!includeHumiditySensor)
            {
                humiditySensor = null;
            }

            if (!includeIlluminanceSensor)
            {
                illuminanceSensor = null;
            }

            if (!includeThermostat)
            {
                thermostat = null;
            }

            if (!includeKeypad)
            {
                keypad = null;
            }

            var device = new ReadOnlyDeviceState("Sample Device", address, location, null, true, type, currentAction, toggle, dimmer, binarySensor, powerSensor, temperatureSensor, humiditySensor, illuminanceSensor, thermostat, keypad);

            return device;
        }

        public static IDeviceState GenerateExampleDevice()
        {
            return GenerateExampleDevice(DeviceType.Controller, true, true, true, true, true, true, true, true, true, true);
        }
    }
}
