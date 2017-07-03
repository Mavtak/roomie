
namespace Roomie.Common.HomeAutomation.Events
{
    public class StillnessDetected : BinarySensorValueChanged
    {
        public new const string Key = "Stillness Detected";

        public override string Name
        {
            get
            {
                return Key;
            }
        } 
    }
}
