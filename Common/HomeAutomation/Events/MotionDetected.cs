
namespace Roomie.Common.HomeAutomation.Events
{
    public class MotionDetected : DevicePowerChanged
    {
        public new const string Key = "Motion Detected";

        public override string Name
        {
            get
            {
                return Key;
            }
        } 
    }
}
