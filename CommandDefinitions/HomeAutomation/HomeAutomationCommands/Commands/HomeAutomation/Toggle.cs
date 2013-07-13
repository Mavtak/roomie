
using Roomie.Common.HomeAutomation.ToggleSwitches;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    public class Toggle : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var device = context.Device;

            if (device.ToggleSwitch.Power == null)
            {
                device.Poll();
            }

            switch (device.ToggleSwitch.Power)
            {
                case ToggleSwitchPower.On:
                    device.ToggleSwitch.PowerOn();
                    break;

                    case ToggleSwitchPower.Off:
                    device.ToggleSwitch.PowerOff();
                    break;
            }
        }
    }
}
