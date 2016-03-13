using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.WeMoCommands.Commands
{
    [Group("WeMo")]
    public class RegisterNetwork : HomeAutomationCommand
    {
        protected override void Execute_HomeAutomationDefinition(HomeAutomationCommandContext context)
        {
            var interpreter = context.Interpreter;
            var networks = context.Networks;

            var networkContext = new HomeAutomationNetworkContext(context.Engine, context.ThreadPool);
            var network = new WeMoNetwork(networkContext);

            networks.Add(network);

            interpreter.WriteEvent("Done.");
        }
    }
}
