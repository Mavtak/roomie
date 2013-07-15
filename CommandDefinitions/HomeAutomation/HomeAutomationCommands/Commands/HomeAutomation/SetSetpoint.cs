using Roomie.CommandDefinitions.HomeAutomationCommands.Attributes;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Temperature;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    [TemperatureParameter("Temperature")]
    [SetpointTypeParameter]
    public class SetSetpoint : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var device = context.Device;
            var scope = context.Scope;
            var temperature = scope.GetValue("Temperature").ToTemperature();
            var setpointType = scope.GetValue("Setpoint").ToSetpointType();

            device.Thermostat.Setpoints.SetSetpoint(setpointType, temperature);
        }
    }
}
