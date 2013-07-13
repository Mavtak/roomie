using Roomie.Common.HomeAutomation.DimmerSwitches;

namespace Roomie.Common.HomeAutomation
{
    public interface IDimmerSwitch : IDimmerSwitchState
    {
        void Poll();
        void SetPower(int power);
    }
}
