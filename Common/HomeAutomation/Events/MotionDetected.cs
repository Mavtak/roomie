
namespace Roomie.Common.HomeAutomation.Events
{
    public class MotionDetected : BinarySensorValueChanged
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
