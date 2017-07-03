using System;

namespace Roomie.Common.HomeAutomation.Events
{
    public interface IEvent
    {
        IHasName Entity { get; }
        IEventType Type { get; }
        DateTime TimeStamp { get; }
        IEventSource Source { get; }
    }
}
