
namespace Roomie.Common.HomeAutomation.ToggleSwitches
{
    public class ReadOnlyToggleSwitchState : IToggleSwitchState
    {
        public bool IsOn { get; private set; }
        public bool IsOff { get; private set; }

        public static ReadOnlyToggleSwitchState CopyFrom(IToggleSwitchState source)
        {
            var result = new ReadOnlyToggleSwitchState
            {
                IsOn = source.IsOn,
                IsOff = source.IsOff
            };

            return result;
        }
    }
}
