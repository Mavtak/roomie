using Roomie.CommandDefinitions.ControlThinkCommands;
using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.ZWave.ControlThinkCommands.Commands.ControlThink
{
    [Description("This command attempts to connect to a ControlThink USB Z-Wave adapater.")]
    public class RegisterNetwork : HomeAutomationCommand
    {
        protected override void Execute_HomeAutomation(HomeAutomationCommandContext context)
        {
            var interpreter = context.Interpreter;
            var networks = context.Networks;

            interpreter.WriteEvent("Searching for Z-Wave network adapater...");

            var network = new ZWaveNetwork();
            networks.Add(network);

            interpreter.WriteEvent("Done.");
        }
    }
}
