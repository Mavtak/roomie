using Roomie.CommandDefinitions.HomeAutomationCommands.Attributes;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    [PowerParameter]
    public class Dim : SingleDeviceControlCommand
    {
        protected override void Execute_HomeAutomationNetwork(HomeAutomationCommandContext context)
        {
            var scope = context.Scope;
            var device = context.Device;

            var power = scope.GetInteger("Power");

            device.Power = power;
        }
    }
}
