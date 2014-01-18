using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Roomie.Common.HomeAutomation.Events
{
    public class History<TEvent> : IHistory<TEvent>
        where TEvent : IEvent
    {
        protected LinkedList<TEvent> Events;

        public History()
        {
            Events = new LinkedList<TEvent>();
        }

        public void Add(TEvent @event)
        {
            lock (Events)
            {
                Events.AddLast(@event);
            }
        }

        public IEnumerable<TEvent> GetMatches(params Func<TEvent, bool>[] filters)
        {
            if (filters.Length == 0)
            {
                return new TEvent[0];
            }

            lock (Events)
            {
                var results = filters.Aggregate(Events.AsEnumerable(), (current, filter) => current.Where(filter));
                results = results.ToArray();

                return results;
            }
        }

        //TODO: probably remove these
        public IEnumerator<TEvent> GetEnumerator()
        {
            return Events.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
