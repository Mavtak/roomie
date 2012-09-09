
namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    public class RemoveDevice : HomeAutomationNetworkCommand
    {
        protected override void Execute_HomeAutomationNetwork(HomeAutomationCommandContext context)
        {
            var interpreter = context.Interpreter;
            var network = context.Network;

            interpreter.WriteEvent("Waiting for device.  Press its button to remove it from its network.");

            var device = network.RemoveDevice();

            interpreter.WriteEvent("Device removed: " + device);

            if (WebHookConnector.WebHookPresent(context))
            {
                context.AddSyncWithCloud();
            }
        }
    }
}
