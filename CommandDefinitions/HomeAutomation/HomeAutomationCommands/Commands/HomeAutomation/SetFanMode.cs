using Roomie.CommandDefinitions.HomeAutomationCommands.Attributes;
using Roomie.Common.HomeAutomation.Thermostats.Fans;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    [FanModeParameter]
    public class SetThermostatFanMode : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var device = context.Device;
            var scope = context.Scope;
            var fanMode = scope.GetValue("FanMode").ToThermostatFanMode();

            device.Thermostat.Fan.SetMode(fanMode);
        }
    }
}
