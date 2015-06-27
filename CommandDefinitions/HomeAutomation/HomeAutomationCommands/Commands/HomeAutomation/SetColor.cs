using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    [ColorParameter("Color")]
    public class SetColor : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var scope = context.Scope;
            var device = context.Device;
            var color = scope.ReadParameter("Color").ToColor();

            device.ColorSwitch.SetValue(color);
        }
    }
}
