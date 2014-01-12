using System;
using System.Xml.Linq;

namespace Roomie.Common.HomeAutomation.Keypads.Buttons
{
    public class ReadOnlyKeypadButtonState : IKeypadButtonState
    {
        public string Id { get; private set; }
        public bool? Pressed { get; private set; }

        public ReadOnlyKeypadButtonState()
        {
        }

        public ReadOnlyKeypadButtonState(string id, bool? pressed)
        {
            Id = id;
            Pressed = pressed;
        }

        public static ReadOnlyKeypadButtonState CopyFrom(IKeypadButtonState source)
        {
            var result = new ReadOnlyKeypadButtonState
            {
                Id = source.Id,
                Pressed = source.Pressed
            };

            return result;
        }

        public static ReadOnlyKeypadButtonState FromXElement(XElement element)
        {
            var id = element.GetAttributeStringValue("Id");
            var pressed = element.GetAttributeStringValue("Pressed");

            var result = new ReadOnlyKeypadButtonState
            {
                Id = id
            };

            if (pressed != null)
            {
                result.Pressed = Convert.ToBoolean(pressed);
            }

            return result;
        }

        public override string ToString()
        {
            return this.Describe();
        }
    }
}
