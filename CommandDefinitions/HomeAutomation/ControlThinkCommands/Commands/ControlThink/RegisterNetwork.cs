using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.ControlThinkCommands.Commands.ControlThink
{
    [Description("This command attempts to connect to a ControlThink USB Z-Wave adapater.")]
    public class RegisterNetwork : HomeAutomationCommand
    {
        protected override void Execute_HomeAutomationDefinition(HomeAutomationCommandContext context)
        {
            var interpreter = context.Interpreter;
            var networks = context.Networks;

            interpreter.WriteEvent("Searching for Z-Wave network adapater...");

            var network = new ZWaveNetwork(new HomeAutomationNetworkContext(context.Engine, context.ThreadPool));
            networks.Add(network);

            interpreter.WriteEvent("Done.");
        }
    }
}
