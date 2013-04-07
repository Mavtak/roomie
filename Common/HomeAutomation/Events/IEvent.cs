using System;

namespace Roomie.Common.HomeAutomation.Events
{
    public interface IEvent
    {
        HomeAutomationEntity Entity { get; }
        IEventType Type { get; }
        DateTime TimeStamp { get; }
        IEventSource Source { get; }
    }
}
