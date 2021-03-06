﻿using System;
using System.Collections.Generic;
using System.Linq;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.TextUtilities;
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
                    Math.Max(history.Max(x => ((x.Entity == null || x.Entity.Name == null) ? string.Empty : x.Entity.Name).Length), headers[1].Length),
                    Math.Max(history.Max(x => ((x.Type == null) ? string.Empty : x.Type.Name).Length), headers[2].Length),
                    Math.Max(history.Max(x => ((x.Source == null || x.Source.Name == null) ? string.Empty : x.Source.Name).Length), headers[3].Length),
                    ExtraLength(history)
                });

            interpreter.WriteEvent(tableBuilder.StartOfTable(headers));

            foreach (var @event in history)
            {
                var extra = GetExtra(@event);

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

        private static string GetExtra(IEvent @event)
        {
            var result = string.Empty;

            var deviceEvent = @event as IDeviceEvent;
            if (deviceEvent != null)
            {
                result = deviceEvent.State.Describe();
            }

            return result;
        }

        private static int ExtraLength(IEnumerable<IEvent> history)
        {
            var longest = 0;

            foreach (var @event in history)
            {
                var extra = GetExtra(@event);
                longest = Math.Max(longest, extra.Length);
            }

            return longest;
        }
    }
}
