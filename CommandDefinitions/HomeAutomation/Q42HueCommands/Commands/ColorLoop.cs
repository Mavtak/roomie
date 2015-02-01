using Q42.HueApi;
using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Desktop.Engine.Commands;

namespace Q42HueCommands.Commands
{
    [Description("Set the device to loop through the rainbow, starting at its current color.")]
    [Group("Q42Hue")]
    public class ColorLoop : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var device = context.Device as Q42HueDevice;
            var command = new LightCommand
            {
                Effect = Effect.ColorLoop,
            };

            device.SendCommand(command);
        }
    }
}
