
namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    [NetworkParameter]
    public class AddDevice : HomeAutomationNetworkCommand
    {
        protected override void Execute_HomeAutomationNetwork(HomeAutomationCommandContext context)
        {
            var engine = context.Engine;
            var interpreter = context.Interpreter;
            var network = context.Network;

            interpreter.WriteEvent("Waiting for device.  Press its button to add it to the network.");

            var device = network.AddDevice();

            interpreter.WriteEvent("Device Added: " + device);

            if (WebHookConnector.WebHookPresent(engine))
            {
                context.AddSyncWithCloud();
            }
        }

        
    }
}