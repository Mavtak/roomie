
namespace Roomie.Common.HomeAutomation.Events
{
    public class PoweredOn : DevicePowerChanged
    {
        public new const string Key = "Powered On";

        public override string Name
        {
            get
            {
                return Key;
            }
        } 
    }
}
