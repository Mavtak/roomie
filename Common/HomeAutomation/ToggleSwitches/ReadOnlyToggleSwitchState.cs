
namespace Roomie.Common.HomeAutomation.ToggleSwitches
{
    public class ReadOnlyToggleSwitchState : IToggleSwitchState
    {
        public ToggleSwitchPower? Power { get; private set; }

        public ReadOnlyToggleSwitchState()
        {
        }

        public ReadOnlyToggleSwitchState(ToggleSwitchPower? power)
        {
            Power = power;
        }

        public static ReadOnlyToggleSwitchState CopyFrom(IToggleSwitchState source)
        {
            var result = new ReadOnlyToggleSwitchState
            {
                Power = source.Power
            };

            return result;
        }
    }
}
