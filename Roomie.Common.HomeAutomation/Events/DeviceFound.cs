
namespace Roomie.Common.HomeAutomation.Events
{
    public class DeviceFound : IEventType
    {
        public const string Key = "Device Found";

        public string Name
        {
            get
            {
                return Key;
            }
        }
    }
}
