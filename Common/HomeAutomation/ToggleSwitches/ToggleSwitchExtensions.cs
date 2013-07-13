using System.Text;

namespace Roomie.Common.HomeAutomation.ToggleSwitches
{
    public static class ToggleSwitchExtensions
    {
        public static IToggleSwitchState Copy(this IToggleSwitchState state)
        {
            return ReadOnlyToggleSwitchState.CopyFrom(state);
        }

        public static string Describe(this IToggleSwitchState state)
        {
            var result = new StringBuilder();

            if (state == null)
            {
                return result.ToString();
            }

            switch (state.Power)
            {
                case ToggleSwitchPower.On:
                    result.Append("on");
                    break;

                case ToggleSwitchPower.Off:
                    result.Append("off");
                    break;
            }

            return result.ToString();
        }
    }
}
