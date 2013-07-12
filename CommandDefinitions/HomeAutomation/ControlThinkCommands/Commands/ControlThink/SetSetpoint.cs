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
            var device = context.Device;
            var scope = context.Scope;
            var interpreter = context.Interpreter;
            var temperature = scope.GetValue("Temperature").ToTemperature();
            var type = scope.GetValue("Setpoint").ToSetpointType();

            interpreter.WriteEvent("Setting " + device + " to " + temperature);

            device.Thermostat.SetSetpoint(type, temperature);
        }
    }
}
