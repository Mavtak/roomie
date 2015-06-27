using Roomie.Common.Measurements.Power;
using Roomie.Desktop.Engine;
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
            var scope = context.Scope;
            var power = scope.GetValue("Power").ToInteger();
            var name = scope.GetValue("Name").Value;
            var device = context.Device;

            device.CurrentStateGenerator.AddStep(new WattsPower(power), name);
        }
    }
}
