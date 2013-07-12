using Roomie.CommandDefinitions.HomeAutomationCommands.Attributes;
using Roomie.Common.HomeAutomation.Thermostats;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    [FanModeParameter]
    public class SetFanMode : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var device = context.Device;
            var scope = context.Scope;
            var fanMode = scope.GetValue("FanMode").ToFanMode();

            device.Thermostat.SetFanMode(fanMode);
        }
    }
}
