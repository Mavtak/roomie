using Roomie.Common.Color;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    [ColorParameter("Color")]
    public class SetColor : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var scope = context.Scope;
            var device = context.Device;
            var color = scope.GetValue("Color").ToColor();

            device.ColorSwitch.SetValue(color);
        }
    }
}
