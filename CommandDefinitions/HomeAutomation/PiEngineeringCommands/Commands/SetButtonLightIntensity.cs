using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.PiEngineeringCommands.Commands
{
    [ByteParameter("BluePower")]
    [ByteParameter("RedPower")]
    [Group("PiEngineering")]
    public class SetButtonLightIntensity : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var device = context.Device as PiEngineeringDevice;
            var scope = context.Scope;
            var bluePower = scope.GetValue("BluePower").ToByte();
            var redPower = scope.GetValue("RedPower").ToByte();

            device.SetButtonLightIntensity(bluePower, redPower);
        }
    }
}
