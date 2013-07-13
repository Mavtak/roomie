using System.Collections.Generic;
using System.Linq;
using Roomie.Common.HomeAutomation.DimmerSwitches;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.ToggleSwitches;
using Roomie.Common.TextUtilities;
using System;
using System.Text;
using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    public class ListEvents : HomeAutomationNetworkCommand
    {
        protected override void Execute_HomeAutomationNetworkDefinition(HomeAutomationCommandContext context)
        {
            var network = context.Network;
            var interpreter = context.Interpreter;
            var history = network.Context.History;

            WriteHistory(history, interpreter);
        }

        internal static void WriteHistory(IEnumerable<IEvent> history, RoomieCommandInterpreter interpreter)
        {
            var headers = new[] {"Time Stamp", "Entity", "Type", "Source", ""};
            var tableBuilder = new TextTable(new int[]
                {
                    Math.Max(history.Max(x => x.TimeStamp.ToLocalTime().ToString().Length), headers[0].Length),
                    Math.Max(history.Max(x => ((x.Entity == null) ? string.Empty : x.Entity.Name).Length), headers[1].Length),
                    Math.Max(history.Max(x => ((x.Type == null) ? string.Empty : x.Type.Name).Length), headers[2].Length),
                    Math.Max(history.Max(x => ((x.Source == null) ? string.Empty : x.Source.Name).Length), headers[3].Length),
                    10
                });

            interpreter.WriteEvent(tableBuilder.StartOfTable(headers));

            foreach (var @event in history)
            {
                var extra = string.Empty;

                var deviceEvent = @event as IDeviceEvent;
                if (deviceEvent != null)
                {
                    if (deviceEvent.ToggleSwitchState != null)
                    {
                        extra = deviceEvent.ToggleSwitchState.Describe();
                    }
                    else if (deviceEvent.DimmerSwitchState != null)
                    {
                        extra = deviceEvent.DimmerSwitchState.Describe();
                    }
                    else if(deviceEvent.ThermostatState != null)
                    {
                        extra = deviceEvent.ThermostatState.Describe();
                    }
                }

                var line = tableBuilder.ContentLine(
                    @event.TimeStamp.ToLocalTime().ToString(),
                    (@event.Entity == null) ? string.Empty : @event.Entity.Name,
                    (@event.Type == null) ? string.Empty : @event.Type.Name,
                    (@event.Source == null) ? string.Empty : @event.Source.Name,
                    extra
                    );

                interpreter.WriteEvent(line);
            }

            interpreter.WriteEvent(tableBuilder.EndOfTable());
        }
    }
}
