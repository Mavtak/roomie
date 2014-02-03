
namespace Roomie.Common.HomeAutomation.DimmerSwitches
{
    public interface IMultilevelSwitchActions
    {
        void Poll();
        void SetPower(int power);
    }
}
