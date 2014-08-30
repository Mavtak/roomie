using System.Xml.Linq;
using Roomie.Common.Color;

namespace Roomie.Common.HomeAutomation.ColorSwitch
{
    public class ReadOnlyColorSwitchState : IColorSwitchState
    {
        public IColor Value { get; private set; }

        private ReadOnlyColorSwitchState()
        {
        }

        public ReadOnlyColorSwitchState(IColor value)
        {
            Value = value;
        }

        public static ReadOnlyColorSwitchState Blank()
        {
            var result = new ReadOnlyColorSwitchState();

            return result;
        }

        public static ReadOnlyColorSwitchState CopyFrom(IColorSwitchState source)
        {
            var result = new ReadOnlyColorSwitchState
            {
                Value = source.Value
            };

            return result;
        }

        public static ReadOnlyColorSwitchState FromXElement(XElement element)
        {
            var value = element.GetAttributeStringValue("Value").ToColor();

            var result = new ReadOnlyColorSwitchState
            {
                Value = value
            };

            return result;
        }
    }
}
