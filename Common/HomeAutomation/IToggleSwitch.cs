
namespace Roomie.Common.HomeAutomation
{
    public interface IToggleSwitch
    {
        void PowerOn();
        void PowerOff();
        bool IsOn { get; }
        bool IsOff { get; }
    }
}
