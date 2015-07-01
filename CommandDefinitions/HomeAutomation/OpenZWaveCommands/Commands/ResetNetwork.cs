using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.Commands
{
    [BooleanParameter(ReturnRouteKey, true)]
    [Group("OpenZWave")]
    public class ResetNetwork : HomeAutomationNetworkCommand
    {
        private const string ReturnRouteKey = "ReturnRoute";

        protected override void Execute_HomeAutomationNetworkDefinition(HomeAutomationCommandContext context)
        {
            var interpreter = context.Interpreter;
            var network = context.Network as OpenZWaveNetwork;

            interpreter.WriteEvent("Resetting Network " + network);

            network.Reset();

            interpreter.WriteEvent("Done");
        }
    }
}
