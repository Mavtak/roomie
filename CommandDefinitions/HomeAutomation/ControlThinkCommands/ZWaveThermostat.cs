using System;
using ControlThink.ZWave.Devices;
using ControlThink.ZWave.Devices.Specific;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.Temperature;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    public class ZWaveThermostat : IThermostat
    {
        private ZWaveDevice _device;

        public ZWaveThermostat(ZWaveDevice device)
        {
            _device = device;
        }

        public void SetSetpoint(SetpointType setpointType, ITemperature temperature)
        {
            var thermostat = _device.BackingObject as GeneralThermostatV2;
            var controlThinkSetpointType = ConvertSetpointType(setpointType);
            var controlThinkTemperature = ConvertTemperature(temperature);

            Action operation = () =>
            {
                thermostat.ThermostatSetpoints[controlThinkSetpointType].Temperature = controlThinkTemperature;
            };

            _device.DoDeviceOperation(operation);
        }

        private static ThermostatSetpointType ConvertSetpointType(SetpointType input)
        {
            switch (input)
            {
                case SetpointType.Heat:
                    return ThermostatSetpointType.Heating1;

                case SetpointType.Cool:
                    return ThermostatSetpointType.Cooling1;

                default:
                    throw new ArgumentException("Could not parse type");
            }
        }

        private static Temperature ConvertTemperature(ITemperature input)
        {
            decimal value;
            TemperatureScale scale;

            if (input is FahrenheitTemperature)
            {
                value = (int)Math.Round(input.Value);
                scale = TemperatureScale.Fahrenheit;
            }
            else
            {
                value = (int)(Math.Round(input.Celsius.Value));
                scale = TemperatureScale.Celsius;
            }

            var result = new Temperature(value, scale);

            return result;
        }
    }
}
