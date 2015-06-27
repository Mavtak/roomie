using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.Commands
{
    [BooleanParameter(ReturnRouteKey, true)]
    [Group("OpenZWave")]
    public class OptimizeDevice : HomeAutomationSingleDeviceCommand
    {
        private const string ReturnRouteKey = "ReturnRoute";

        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var interpreter = context.Interpreter;
            var device = context.Device as OpenZWaveDevice;
            var scope = context.Scope;
            var returnRoute = scope.GetValue(ReturnRouteKey).ToBoolean();

            interpreter.WriteEvent("Optimizing " + device);

            device.OptimizePaths(returnRoute);

            interpreter.WriteEvent("Done");
        }
    }
}
