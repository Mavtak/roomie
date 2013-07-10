using System;
using ControlThink.ZWave.Devices;
using ControlThink.ZWave.Devices.Specific;
using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.CommandDefinitions.HomeAutomationCommands.Attributes;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.Temperature;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.ControlThinkCommands.Commands.ControlThink
{
    [TemperatureParameter("Temperature")]
    [SetpointTypeParameter]
    public class SetSetpoint : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var device = context.Device as ZWaveDevice;
            var scope = context.Scope;
            var interpreter = context.Interpreter;
            var temperature = scope.GetTemperature("Temperature");
            var type = SetpointTypeParser.Parse(scope.GetValue("Setpoint"));

            interpreter.WriteEvent("Setting " + device + " to " + temperature);

            Set(device, type, temperature);
        }

        private static void Set(ZWaveDevice device, SetpointType triggerType, ITemperature temperature)
        {
            var thermostat = device.BackingObject as GeneralThermostatV2;
            var controlThinkSetpointType = ConvertSetpointType(triggerType);
            var controlThinkTemperature = ConvertTemperature(temperature);

            thermostat.ThermostatSetpoints[controlThinkSetpointType].Temperature = controlThinkTemperature;
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
                value = (int) Math.Round(input.Value);
                scale = TemperatureScale.Fahrenheit;
            }
            else
            {
                value = (int) (Math.Round(input.Celsius.Value));
                scale = TemperatureScale.Celsius;
            }

            var result = new Temperature(value, scale);

            return result;
        }
    }
}
