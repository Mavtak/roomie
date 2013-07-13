
namespace Roomie.Common.HomeAutomation.ToggleSwitches
{
    public interface IToggleSwitch : IToggleSwitchState
    {
        void PowerOn();
        void PowerOff();
        void Poll();
    }
}
