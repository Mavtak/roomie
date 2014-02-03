
namespace Roomie.Common.HomeAutomation.BinarySwitches
{
    public interface IBinarySwitchActions
    {
        void SetPower(BinarySwitchPower power);
        void Poll();
    }
}
