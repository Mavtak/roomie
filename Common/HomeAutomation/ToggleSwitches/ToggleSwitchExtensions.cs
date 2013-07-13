
using System.Text;

namespace Roomie.Common.HomeAutomation.ToggleSwitches
{
    public static class ToggleSwitchExtensions
    {
        public static string Describe(this IToggleSwitchState state)
        {
            var result = new StringBuilder();

            if (state == null)
            {
                return result.ToString();
            }

            if (state.IsOn)
            {
                result.Append("on");

                if (state.IsOff)
                {
                    result.Append(" and also somehow ");
                }
            }

            if (state.IsOff)
            {
                result.Append("off");
            }

            return result.ToString();
        }
    }
}
