using System.Text;

namespace Roomie.Common.HomeAutomation.DimmerSwitches
{
    public static class DimmerSwitchStateExtensions
    {
        public static ReadOnlyDimmerSwitchState Copy(this IDimmerSwitchState state)
        {
            return ReadOnlyDimmerSwitchState.CopyFrom(state);
        }

        public static string Describe(this IDimmerSwitchState state)
        {
            var result = new StringBuilder();

            if (state == null)
            {
                return result.ToString();
            }

            var percentage = state.CalculatePowerPercentage();
            if (percentage != null)
            {
                result.Append(percentage);
                result.Append("%");
            }

            return result.ToString();
        }

        public static int? CalculatePowerPercentage(this IDimmerSwitchState state)
        {
            if (state == null)
            {
                return null;
            }

            if (state.Power == null)
            {
                return null;
            }

            var result = state.Power*100/(state.MaxPower > 0 ? state.MaxPower : 100);

            return result;
        }
    }
}
