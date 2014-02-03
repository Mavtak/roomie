using Roomie.Common.HomeAutomation.BinarySwitches;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    public class PowerOff : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var device = context.Device;

            device.ToggleSwitch.SetPower(BinarySwitchPower.Off);
        }
    }
}
