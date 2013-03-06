
namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    public class RegisterPowerChangedCommand : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var originalCommand = context.OriginalCommand;
            var commands = originalCommand.InnerCommands;
            var device = context.Device;

            device.PowerChangedCommands.Add(commands);
        }
    }
}