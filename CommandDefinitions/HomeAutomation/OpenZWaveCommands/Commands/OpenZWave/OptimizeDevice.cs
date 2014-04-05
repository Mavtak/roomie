using Roomie.CommandDefinitions.HomeAutomationCommands;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.Commands.OpenZWave
{
    public class OptimizeDevice : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var interpreter = context.Interpreter;
            var device = context.Device as OpenZWaveDevice;

            interpreter.WriteEvent("Optimizing " + device);

            device.OptimizePaths();

            interpreter.WriteEvent("Done");
        }
    }
}
