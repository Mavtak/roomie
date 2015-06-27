using Roomie.CommandDefinitions.HomeAutomationCommands.Attributes;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    [TemperatureParameter("Temperature")]
    [SetpointTypeParameter]
    public class SetSetpoint : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var device = context.Device;
            var temperature = context.ReadParameter("Temperature").ToTemperature();
            var setpointType = context.ReadParameter("Setpoint").ToThermostatSetpointType();

            device.Thermostat.Setpoints.SetSetpoint(setpointType, temperature);
        }
    }
}
