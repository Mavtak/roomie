
namespace Roomie.Common.HomeAutomation.DimmerSwitches
{
    public interface IDimmerSwitchActions
    {
        void Poll();
        void SetPower(int power);
    }
}
