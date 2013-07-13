
namespace Roomie.Common.HomeAutomation.DimmerSwitches
{
    public interface IDimmerSwitch : IDimmerSwitchState
    {
        void Poll();
        void SetPower(int power);
    }
}
