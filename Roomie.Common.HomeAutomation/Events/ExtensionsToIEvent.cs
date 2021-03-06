﻿using System.Collections.Generic;
using System.Linq;

namespace Roomie.Common.HomeAutomation.Events
{
    public static class ExtensionsToIEvent
    {
        public static IEnumerable<TEvent> FilterEntity<TEvent>(this IEnumerable<TEvent> history, IHasName entity)
            where TEvent : class, IEvent
        {
            var results = history.Where(x => ReferenceEquals(x.Entity, entity));

            return results;
        }

        public static IEnumerable<TEvent> FilterType<TEvent>(this IEnumerable<TEvent> history, IEventType type)
            where TEvent : class, IEvent
        {
            var results = history.Where(x => x.Type.Matches(type));

            return results;
        }
    }
}
