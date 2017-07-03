
namespace Roomie.Common.HomeAutomation.Events
{
    public class NetworkDisconnected : IEventType
    {
        public const string Key = "Network Disconnected";

        public string Name
        {
            get
            {
                return Key;
            }
        }
    }
}
