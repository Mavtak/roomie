
namespace Roomie.Common.HomeAutomation.Events
{
    public interface IDeviceEvent : IEvent
    {
        Device Device { get; }
        IDeviceState State { get; }
    }
}
