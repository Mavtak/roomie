
namespace Roomie.Common.HomeAutomation.DimmerSwitches
{
    public interface IMultilevelSwitchState
    {
        int? Power { get;}
        int? MaxPower { get; }

        void Update(IMultilevelSwitchState state);
    }
}
