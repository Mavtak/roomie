using Roomie.Common.HomeAutomation.Events;
using System.Text;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    public class ListEvents : HomeAutomationNetworkCommand
    {
        protected override void Execute_HomeAutomationNetworkDefinition(HomeAutomationCommandContext context)
        {
            var network = context.Network;
            var interpreter = context.Interpreter;

            foreach (var @event in network.Context.History)
            {
                var message = new StringBuilder();
                message.Append(@event.TimeStamp);
                message.Append(" ");
                message.Append(@event.Type.Name);
                message.Append(" ");
                message.Append(@event.Entity.Name);

                var deviceEvent = @event as IDeviceEvent;
                if (deviceEvent != null)
                {
                    if (deviceEvent.Type is DevicePowerChanged)
                    {
                        message.Append(" Power=");
                        message.Append(deviceEvent.Power);
                    }
                }

                interpreter.WriteEvent(message.ToString());
            }
        }
    }
}
