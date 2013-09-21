
namespace Roomie.Common.HomeAutomation.Events
{
    public interface IDeviceEvent : IEvent
    {
        IDevice Device { get; }
        IDeviceState State { get; }
    }
}
