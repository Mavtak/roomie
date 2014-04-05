using Roomie.CommandDefinitions.HomeAutomationCommands;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.Commands.OpenZWave
{
    public class OptimizeNetwork : HomeAutomationNetworkCommand
    {
        protected override void Execute_HomeAutomationNetworkDefinition(HomeAutomationCommandContext context)
        {
            var interpreter = context.Interpreter;
            var network = context.Network as OpenZWaveNetwork;

            interpreter.WriteEvent("Optimizing " + network);

            network.OptimizePaths();

            interpreter.WriteEvent("Done");
        }
    }
}
