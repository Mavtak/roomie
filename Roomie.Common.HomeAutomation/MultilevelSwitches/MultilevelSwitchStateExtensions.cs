using System.Text;
using System.Xml.Linq;

namespace Roomie.Common.HomeAutomation.MultilevelSwitches
{
    public static class MultilevelSwitchStateExtensions
    {
        public static ReadOnlyMultilevelSwitchState Copy(this IMultilevelSwitchState state)
        {
            return ReadOnlyMultilevelSwitchState.CopyFrom(state);
        }

        public static string Describe(this IMultilevelSwitchState state)
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

        public static int? CalculatePowerPercentage(this IMultilevelSwitchState state)
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

        public static XElement ToXElement(this IMultilevelSwitchState state, string nodeName = "DimmerSwitch")
        {
            var result = new XElement("DimmerSwitch");

            if (state.Power != null)
            {
                result.Add(new XAttribute("Power", state.Power));
            }

            if (state.MaxPower != null)
            {
                result.Add(new XAttribute("MaxPower", state.MaxPower));
            }

            return result;
        }

        public static ReadOnlyMultilevelSwitchState ToDimmerSwitch(this XElement element)
        {
            return ReadOnlyMultilevelSwitchState.FromXElement(element);
        }
    }
}
