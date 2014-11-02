using System.Collections.Generic;
using System.Linq;

namespace Roomie.Common.HomeAutomation.Events
{
    public static class ExtensionsToIHistory
    {
        public static IEnumerable<TEvent> FilterEntity<TEvent>(this IEnumerable<TEvent> history, IHasName entity)
            where TEvent : class, IEvent
        {
            var results = history.Where(x => ReferenceEquals(x.Entity, entity));

            return results;
        }
    }
}
