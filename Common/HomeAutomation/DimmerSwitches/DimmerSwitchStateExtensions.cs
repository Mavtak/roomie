using System.Text;

namespace Roomie.Common.HomeAutomation.DimmerSwitches
{
    public static class DimmerSwitchStateExtensions
    {
        public static string Describe(this IDimmerSwitchState state)
        {
            var result = new StringBuilder();


            if (state == null)
            {
                return result.ToString();
            }

            if (state.Power != null)
            {
                result.Append(Utilities.CalculatePowerPercentage(state.Power, state.MaxPower));
                result.Append("%");
            }

            return result.ToString();
        }
    }
}
