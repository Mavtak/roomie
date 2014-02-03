using System.Xml.Linq;

namespace Roomie.Common.HomeAutomation.BinarySwitches
{
    public class ReadOnlyBinarySwitchSwitchState : IBinarySwitchState
    {
        public BinarySwitchPower? Power { get; private set; }

        private ReadOnlyBinarySwitchSwitchState()
        {
        }

        public ReadOnlyBinarySwitchSwitchState(BinarySwitchPower? power)
        {
            Power = power;
        }

        public static ReadOnlyBinarySwitchSwitchState CopyFrom(IBinarySwitchState source)
        {
            var result = new ReadOnlyBinarySwitchSwitchState
            {
                Power = source.Power
            };

            return result;
        }

        public static ReadOnlyBinarySwitchSwitchState FromXElement(XElement element)
        {
            var power = element.GetAttributeStringValue("Power").ToToggleSwitchPower();

            var result = new ReadOnlyBinarySwitchSwitchState
            {
                Power = power
            };

            return result;
        }
    }
}
