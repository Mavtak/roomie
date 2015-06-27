using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;

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
            var bluePower = scope.ReadParameter("BluePower").ToByte();
            var redPower = scope.ReadParameter("RedPower").ToByte();

            device.SetButtonLightIntensity(bluePower, redPower);
        }
    }
}
