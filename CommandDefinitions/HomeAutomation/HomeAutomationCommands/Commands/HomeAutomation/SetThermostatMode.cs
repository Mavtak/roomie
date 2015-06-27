using Roomie.CommandDefinitions.HomeAutomationCommands.Attributes;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    [ThermostatModeParameter]
    public class SetThermostatMode : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var device = context.Device;
            var scope = context.Scope;
            var mode = scope.ReadParameter("ThermostatMode").ToThermostatMode();

            device.Thermostat.Core.SetMode(mode);
        }
    }
}
