using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Roomie.Common.HomeAutomation.Keypads.Buttons;

namespace Roomie.Common.HomeAutomation.Keypads
{
    public class ReadOnlyKeypadState : IKeypadState
    {
        public IEnumerable<IKeypadButtonState> Buttons { get; private set; }

        public ReadOnlyKeypadState()
        {
        }

        public ReadOnlyKeypadState(IEnumerable<IKeypadButtonState> buttons)
        {
            Buttons = buttons.Copy();
        }

        public static IKeypadState CopyFrom(IKeypadState source)
        {
            var result = new ReadOnlyKeypadState();

            if (source.Buttons != null)
            {
                result.Buttons = source.Buttons.Copy();
            }

            return result;
        }

        public static ReadOnlyKeypadState FromXElement(XElement element)
        {
            var buttonNodes = element.Descendants();

            var result = new ReadOnlyKeypadState();

            if (buttonNodes.Any())
            {
                result.Buttons = buttonNodes.ToButtons();
            }

            return result;
        }

    }
}
