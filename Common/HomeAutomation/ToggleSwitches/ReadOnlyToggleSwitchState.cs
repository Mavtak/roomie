
using System.Xml.Linq;

namespace Roomie.Common.HomeAutomation.ToggleSwitches
{
    public class ReadOnlyToggleSwitchState : IToggleSwitchState
    {
        public ToggleSwitchPower? Power { get; private set; }

        private ReadOnlyToggleSwitchState()
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

        public static ReadOnlyToggleSwitchState FromXElement(XElement element)
        {
            var power = element.GetAttributeStringValue("Power").ToToggleSwitchPower();

            var result = new ReadOnlyToggleSwitchState
            {
                Power = power
            };

            return result;
        }
    }
}
