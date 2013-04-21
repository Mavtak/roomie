using Roomie.Common.HomeAutomation.Events;
using System.Text;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    public class ListDeviceEvents : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var network = context.Network;
            var device = context.Device;
            var history = network.Context.History;
            var interpreter = context.Interpreter;
            var deviceHistory = history.DeviceEvents.FilterEntity(device);

            ListEvents.WriteHistory(deviceHistory, interpreter);
        }
    }
}
