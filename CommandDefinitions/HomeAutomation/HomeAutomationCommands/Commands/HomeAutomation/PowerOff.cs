
namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    public class PowerOff : SingleDeviceControlCommand
    {
        protected override void Execute_HomeAutomationNetwork(HomeAutomationCommandContext context)
        {
            var device = context.Device;

            device.PowerOff();
        }
    }
}
