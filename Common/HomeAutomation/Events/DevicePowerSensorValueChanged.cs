
namespace Roomie.Common.HomeAutomation.Events
{
    public class DevicePowerSensorValueChanged : DeviceStateChanged
    {
        public new const string Key = "Device Power Sensor Value Changed";

        public override string Name
        {
            get
            {
                return Key;
            }
        }
    }
}
