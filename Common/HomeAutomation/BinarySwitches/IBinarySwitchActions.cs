
namespace Roomie.Common.HomeAutomation.BinarySwitches
{
    public interface IBinarySwitchActions
    {
        void PowerOn();
        void PowerOff();
        void Poll();
    }
}
