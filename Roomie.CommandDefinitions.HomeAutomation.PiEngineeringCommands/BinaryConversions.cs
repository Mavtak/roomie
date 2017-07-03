using System.Collections.Generic;
using System.Linq;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.Keypads.Buttons;

namespace Roomie.CommandDefinitions.PiEngineeringCommands
{
    public class BinaryConversions
    {
        public static IKeypadState ConvertKeypad(byte[] data)
        {
            var buttons = ConvertButtons(data).ToArray();

            var result = new ReadOnlyKeypadState(buttons);

            return result;
        }

        public static IEnumerable<IKeypadButtonState> ConvertButtons(byte[] data)
        {
            var relevantBytes = data.Skip(3).Take(4);

            var row = 1;
            var column = 'A';

            foreach (var chunk in relevantBytes)
            {
                byte mask = 1;

                for (byte i = 0; i < 6; i++)
                {
                    var id = "" + column + row;
                    var pressed = (chunk & mask) > 0;

                    var buttonState = new ReadOnlyKeypadButtonState(id, pressed);

                    yield return buttonState;

                    row++;
                    mask *= 2;
                }

                row = 1;
                column++;
            }
        }
    }
}
