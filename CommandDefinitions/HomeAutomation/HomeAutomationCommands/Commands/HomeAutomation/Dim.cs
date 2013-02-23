using Roomie.CommandDefinitions.HomeAutomationCommands.Attributes;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    [PowerParameter]
    public class Dim : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var scope = context.Scope;
            var device = context.Device;

            var power = scope.GetInteger("Power");

            device.Power = power;
        }
    }
}
