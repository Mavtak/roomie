
namespace Roomie.Common.HomeAutomation.Events
{
    public class CurrentActionChanged : DeviceStateChanged
    {
        public new const string Key = "Current Action Changed";

        public override string Name
        {
            get
            {
                return Key;
            }
        } 
    }
}
