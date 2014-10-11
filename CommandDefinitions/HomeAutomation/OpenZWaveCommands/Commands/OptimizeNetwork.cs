using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.Commands
{
    [BooleanParameter(ReturnRouteKey, true)]
    [Group("OpenZWave")]
    public class OptimizeNetwork : HomeAutomationNetworkCommand
    {
        private const string ReturnRouteKey = "ReturnRoute";

        protected override void Execute_HomeAutomationNetworkDefinition(HomeAutomationCommandContext context)
        {
            var interpreter = context.Interpreter;
            var network = context.Network as OpenZWaveNetwork;
            var scope = context.Scope;
            var returnRoute = scope.GetValue(ReturnRouteKey).ToBoolean();

            interpreter.WriteEvent("Optimizing " + network);

            network.OptimizePaths(returnRoute);

            interpreter.WriteEvent("Done");
        }
    }
}
