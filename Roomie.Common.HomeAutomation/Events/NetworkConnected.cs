
namespace Roomie.Common.HomeAutomation.Events
{
    public class NetworkConnected : IEventType
    {
        public const string Key = "Network Connected";

        public string Name
        {
            get
            {
                return Key;
            }
        }
    }
}
