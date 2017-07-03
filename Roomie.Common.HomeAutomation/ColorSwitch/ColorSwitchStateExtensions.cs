using System.Text;
using System.Xml.Linq;
using Roomie.Common.Color;

namespace Roomie.Common.HomeAutomation.ColorSwitch
{
    public static class ColorSwitchStateExtensions
    {
        public static IColorSwitchState Copy(this IColorSwitchState state)
        {
            return ReadOnlyColorSwitchState.CopyFrom(state);
        }

        public static string Describe(this IColorSwitchState state)
        {
            var result = new StringBuilder();

            if (state == null)
            {
                return result.ToString();
            }

            if (state.Value == null)
            {
                result.Append("Unknown Color");
            }
            else
            {
                var color = state.Value;

                result.Append(color.ToHexString());

                var name = color.Name;

                if (name != null && name.Value != null)
                {
                    result.Append("(");
                    result.Append(name.Value);
                    result.Append(")");
                }
            }

            return result.ToString();
        }

        public static XElement ToXElement(this IColorSwitchState state, string nodeName = "ColorSwitch")
        {
            var result = new XElement(nodeName);

            if (state.Value != null)
            {
                result.Add(new XAttribute("Value", state.Value.ToHexString()));
            }

            return result;
        }

        public static IColorSwitchState ToColorSwitch(this XElement element)
        {
            return ReadOnlyColorSwitchState.FromXElement(element);
        }
    }
}
