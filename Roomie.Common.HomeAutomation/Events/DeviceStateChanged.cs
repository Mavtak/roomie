
namespace Roomie.Common.HomeAutomation.Events
{
    public class DeviceStateChanged : IEventType
    {
        public const string Key = "Device State Changed";

        public virtual string Name
        {
            get
            {
                return Key;
            }
        }
    }
}
