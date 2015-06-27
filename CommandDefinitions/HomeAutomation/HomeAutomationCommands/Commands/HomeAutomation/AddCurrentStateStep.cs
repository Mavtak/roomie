using Roomie.Common.Measurements.Power;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    [IntegerParameter("Power")]
    [StringParameter("Name")]
    public class AddCurrentStateStep : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var power = context.ReadParameter("Power").ToInteger();
            var name = context.ReadParameter("Name").Value;
            var device = context.Device;

            device.CurrentStateGenerator.AddStep(new WattsPower(power), name);
        }
    }
}
