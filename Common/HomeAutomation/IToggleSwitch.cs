
namespace Roomie.Common.HomeAutomation
{
    public interface IToggleSwitch
    {
        void PowerOn();
        void PowerOff();
        void Poll();
        bool IsOn { get; }
        bool IsOff { get; }
    }
}
