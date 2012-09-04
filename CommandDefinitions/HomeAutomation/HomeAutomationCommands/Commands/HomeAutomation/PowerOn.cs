
namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    public class PowerOn : SingleDeviceControlCommand
    {
        protected override void Execute_HomeAutomationNetwork(HomeAutomationCommandContext context)
        {
            var device = context.Device;

            device.PowerOn();
        }
    }
}
