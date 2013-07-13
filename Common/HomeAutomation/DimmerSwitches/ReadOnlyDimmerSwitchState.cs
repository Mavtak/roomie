
namespace Roomie.Common.HomeAutomation.DimmerSwitches
{
    public class ReadOnlyDimmerSwitchState : IDimmerSwitchState
    {
        public int? Power { get; private set; }
        public int? MaxPower { get; private set; }

        public static ReadOnlyDimmerSwitchState CopyFrom(IDimmerSwitchState source)
        {
            var result = new ReadOnlyDimmerSwitchState
            {
                Power = source.Power,
                MaxPower =  source.MaxPower
            };

            return result;
        }
    }
}
