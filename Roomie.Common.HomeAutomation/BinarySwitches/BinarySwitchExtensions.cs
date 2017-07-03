using System.Text;
using System.Xml.Linq;

namespace Roomie.Common.HomeAutomation.BinarySwitches
{
    public static class BinarySwitchExtensions
    {
        public static IBinarySwitchState Copy(this IBinarySwitchState state)
        {
            return ReadOnlyBinarySwitchSwitchState.CopyFrom(state);
        }

        public static string Describe(this IBinarySwitchState state)
        {
            var result = new StringBuilder();

            if (state == null)
            {
                return result.ToString();
            }

            switch (state.Power)
            {
                case BinarySwitchPower.On:
                    result.Append("on");
                    break;

                case BinarySwitchPower.Off:
                    result.Append("off");
                    break;
            }

            return result.ToString();
        }

        public static XElement ToXElement(this IBinarySwitchState state, string nodeName = "ToggleSwitch")
        {
            var result = new XElement(nodeName);

            if (state.Power != null)
            {
                result.Add(new XAttribute("Power", state.Power));
            }

            return result;
        }

        public static IBinarySwitchState ToToggleSwitch(this XElement element)
        {
            return ReadOnlyBinarySwitchSwitchState.FromXElement(element);
        }
    }
}
