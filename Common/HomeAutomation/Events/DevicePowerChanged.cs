
namespace Roomie.Common.HomeAutomation.Events
{
    public class DevicePowerChanged : DeviceStateChanged
    {
        public new const string Key = "Device Power Changed";

        public override string Name
        {
            get
            {
                return Key;
            }
        }
    }
}
