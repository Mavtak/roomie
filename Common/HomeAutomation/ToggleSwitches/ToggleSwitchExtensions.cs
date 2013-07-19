using System.Text;
using System.Xml.Linq;

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

        public static XElement ToXElement(this IToggleSwitchState state, string nodeName = "ToggleSwitch")
        {
            var result = new XElement(nodeName);

            if (state.Power != null)
            {
                result.Add(new XAttribute("Power", state.Power));
            }

            return result;
        }

        public static IToggleSwitchState ToToggleSwitch(this XElement element)
        {
            return ReadOnlyToggleSwitchState.FromXElement(element);
        }
    }
}
