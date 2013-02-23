
namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    public class Toggle : SingleDeviceControlCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var device = context.Device;

            if (device.IsOff)
            {
                device.PowerOn();
            }
            else
            {
                device.PowerOff();
            }

            //TODO: make this more efficient
            device.Poll();
        }
    }
}
