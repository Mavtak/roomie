using Roomie.Common.HomeAutomation.ToggleSwitches;

namespace Roomie.Common.HomeAutomation
{
    public interface IToggleSwitch : IToggleSwitchState
    {
        void PowerOn();
        void PowerOff();
        void Poll();
    }
}
