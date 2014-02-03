
using Roomie.Common.HomeAutomation.BinarySwitches;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    public class Toggle : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var device = context.Device;

            if (device.BinarySwitch.Power == null)
            {
                device.Poll();
            }

            switch (device.BinarySwitch.Power)
            {
                case BinarySwitchPower.On:
                    device.BinarySwitch.SetPower(BinarySwitchPower.Off);
                    break;

                    case BinarySwitchPower.Off:
                    device.BinarySwitch.SetPower(BinarySwitchPower.On);
                    break;
            }
        }
    }
}
