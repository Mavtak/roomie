using Roomie.Common.HomeAutomation.Events;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    public class ListDeviceEvents : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var device = context.Device;
            var network = device.Network;
            var networkContext = network.Context;
            var history = networkContext.History;
            var interpreter = context.Interpreter;
            var deviceHistory = history.DeviceEvents.FilterEntity(device);

            ListEvents.WriteHistory(deviceHistory, interpreter);
        }
    }
}
