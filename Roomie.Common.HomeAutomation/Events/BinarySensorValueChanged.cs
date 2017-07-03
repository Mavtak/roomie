
namespace Roomie.Common.HomeAutomation.Events
{
    public class BinarySensorValueChanged : DeviceStateChanged
    {
        public new const string Key = "Binary Sensor Value Changed";

        public override string Name
        {
            get
            {
                return Key;
            }
        } 
    }
}
