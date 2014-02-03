
namespace Roomie.Common.HomeAutomation.MultilevelSwitches
{
    public interface IMultilevelSwitchActions
    {
        void Poll();
        void SetPower(int power);
    }
}
