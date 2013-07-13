
namespace Roomie.Common.HomeAutomation.DimmerSwitches
{
    public interface IDimmerSwitchState
    {
        int? Power { get;}
        int? Percentage { get; }
        int? MaxPower { get; }
    }
}
