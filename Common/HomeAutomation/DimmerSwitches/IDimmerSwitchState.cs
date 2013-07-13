
namespace Roomie.Common.HomeAutomation.DimmerSwitches
{
    public interface IDimmerSwitchState
    {
        int? Power { get;}
        int? MaxPower { get; }

        void Update(IDimmerSwitchState state);
    }
}
