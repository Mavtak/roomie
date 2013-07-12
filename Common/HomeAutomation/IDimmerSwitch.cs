
namespace Roomie.Common.HomeAutomation
{
    public interface IDimmerSwitch
    {
        int? Power { get; set; }
        int MaxPower { get; }
        int? Percentage { get; }

        void Poll();
    }
}
