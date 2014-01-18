using System;
using System.Collections.Generic;

namespace Roomie.Common.HomeAutomation.Events
{
    public interface IHistory<TEvent> : IEnumerable<TEvent>
        where TEvent : IEvent
    {
        void Add(TEvent @event);
        IEnumerable<TEvent> GetMatches(params Func<TEvent, bool>[] filters);
    }
}
