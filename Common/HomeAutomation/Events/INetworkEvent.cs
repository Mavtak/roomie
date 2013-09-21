
namespace Roomie.Common.HomeAutomation.Events
{
    public interface INetworkEvent : IEvent
    {
        INetwork Network { get; }
    }
}
