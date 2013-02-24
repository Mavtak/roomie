using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    public class StartupTasks : StartupCommand
    {
        protected override void Execute_StartupDefinition(RoomieCommandContext context)
        {
            var argumentTypes = context.ArgumentTypes;

            argumentTypes.Add(new DeviceAddressParameterType());
        }
    }
}
