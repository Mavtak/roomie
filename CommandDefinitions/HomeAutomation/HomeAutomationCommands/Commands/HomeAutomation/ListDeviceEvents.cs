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
            context.Interpreter.WriteEvent("network: " + network);
            context.Interpreter.WriteEvent("network.Context: " + network.Context);
            context.Interpreter.WriteEvent("network.Context.History: " + network.Context.History);
            var history = network.Context.History;
            var interpreter = context.Interpreter;

            foreach (var @event in history.DeviceEvents.FilterEntity(device))
            {
                //TODO print prettier & DRY
                var message = new StringBuilder();
                message.Append(@event.TimeStamp);
                message.Append(" ");
                message.Append(@event.Type.Name);
                message.Append(" ");
                message.Append(@event.Entity.Name);

                if (@event.Type is DevicePowerChanged)
                {
                    message.Append(" Power=");
                    message.Append(@event.Power);
                }

                interpreter.WriteEvent(message.ToString());
            }
        }
    }
}
