using System.Collections;
using System.Collections.Generic;

namespace Roomie.Common.HomeAutomation.Events
{
    public class History<TEvent> : IHistory<TEvent>
        where TEvent : IEvent
    {
        protected List<TEvent> Events;

        public History()
        {
            Events = new List<TEvent>();
        }

        public void Add(TEvent @event)
        {
            Events.Add(@event);
        }

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
