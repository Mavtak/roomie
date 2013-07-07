
namespace Roomie.Common.HomeAutomation.Events
{
    public class TemperatureChanged : DevicePowerChanged
    {
        public new const string Key = "Temperature Changed";

        public override string Name
        {
            get
            {
                return Key;
            }
        } 
    }
}
