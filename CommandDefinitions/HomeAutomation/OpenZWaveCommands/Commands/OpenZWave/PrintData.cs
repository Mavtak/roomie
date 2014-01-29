using Roomie.CommandDefinitions.HomeAutomationCommands;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.Commands.OpenZWave
{
    public class PrintData : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var interpreter = context.Interpreter;
            var device = context.Device as OpenZWaveDevice;

            foreach (var value in device.Values)
            {
                var text = device.Manager.FormatData(value);

                interpreter.WriteEvent(text);
            }
        }
    }
}
