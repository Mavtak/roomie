
namespace Roomie.Common.HomeAutomation.Events
{
    public class DeviceLost : IEventType
    {
        public const string Key = "Device Lost";

        public string Name
        {
            get
            {
                return Key;
            }
        }
    }
}
