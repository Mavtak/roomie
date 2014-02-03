
namespace Roomie.Common.HomeAutomation.ToggleSwitches
{
    public interface IBinarySwitchActions
    {
        void PowerOn();
        void PowerOff();
        void Poll();
    }
}
