
namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    public class RemoveBrokenDevice : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var interpreter = context.Interpreter;
            var device = context.Device;

            device.Network.RemoveDevice(device);
            
            interpreter.WriteEvent("Device removed: " + device);

            if (WebHookConnector.WebHookPresent(context))
            {
                context.AddSyncWithCloud();
            }
        }
    }
}
