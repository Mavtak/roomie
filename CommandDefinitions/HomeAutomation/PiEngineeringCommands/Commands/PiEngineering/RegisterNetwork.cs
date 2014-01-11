using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.PiEngineeringCommands.Commands.PiEngineering
{
    [Description("This command attempts to connect to P. I. Engineering devices.")]
    public class RegisterNetwork : HomeAutomationCommand
    {
        protected override void Execute_HomeAutomationDefinition(HomeAutomationCommandContext context)
        {
            var interpreter = context.Interpreter;
            var networks = context.Networks;

            interpreter.WriteEvent("Searching for PiEngineering devices");

            var network = new PiEngineeringNetwork(new HomeAutomationNetworkContext(context.Engine, context.ThreadPool));
            networks.Add(network);

            interpreter.WriteEvent("Done.");
        }
    }
}
