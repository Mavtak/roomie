
namespace Roomie.Common.HomeAutomation.Events
{
    public class PoweredOff : DevicePowerChanged
    {
        public new const string Key = "Powered Off";

        public override string Name
        {
            get
            {
                return Key;
            }
        } 
    }
}
