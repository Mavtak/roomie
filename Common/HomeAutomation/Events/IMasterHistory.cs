
namespace Roomie.Common.HomeAutomation.Events
{
    public interface IMasterHistory : IHistory<IEvent>
    {
        IDeviceHistory DeviceEvents { get; }
        INetworkHistory NetworkEvents { get; }
    }
}
