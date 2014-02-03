
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
                case BinarySwitchPower.On:
                    device.ToggleSwitch.PowerOff();
                    break;

                    case BinarySwitchPower.Off:
                    device.ToggleSwitch.PowerOn();
                    break;
            }
        }
    }
}
