using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Roomie.Common.HomeAutomation.Keypads.Buttons;

namespace Roomie.CommandDefinitions.PiEngineeringCommands.Tests
{
    public class BinaryConversionsTests
    {
        [TestCaseSource("ConvertButtonsWorksData")]
        public void ConvertButtonsWorks(byte[] data, IKeypadButtonState[] expected)
        {
            var actual = BinaryConversions.ConvertButtons(data).ToArray();

            Assert.That(actual.Length, Is.EqualTo(expected.Length));

            foreach (var actualButton in actual)
            {
                var expectedButton = expected.FirstOrDefault(x => x.Id == actualButton.Id);
                Assert.That(expectedButton, Is.Not.Null);
                Assert.That(actualButton.Pressed, Is.EqualTo(expectedButton.Pressed));
            }
        }

        public IKeypadButtonState[] Buttons(bool?[] values)
        {
            var result = new[]
            {
                new ReadOnlyKeypadButtonState("A1", values[0]),
                new ReadOnlyKeypadButtonState("A2", values[1]),
                new ReadOnlyKeypadButtonState("A3", values[2]),
                new ReadOnlyKeypadButtonState("A4", values[3]),
                new ReadOnlyKeypadButtonState("A5", values[4]),
                new ReadOnlyKeypadButtonState("A6", values[5]),
                new ReadOnlyKeypadButtonState("B1", values[6]),
                new ReadOnlyKeypadButtonState("B2", values[7]),
                new ReadOnlyKeypadButtonState("B3", values[8]),
                new ReadOnlyKeypadButtonState("B4", values[9]),
                new ReadOnlyKeypadButtonState("B5", values[10]),
                new ReadOnlyKeypadButtonState("B6", values[11]),
                new ReadOnlyKeypadButtonState("C1", values[12]),
                new ReadOnlyKeypadButtonState("C2", values[13]),
                new ReadOnlyKeypadButtonState("C3", values[14]),
                new ReadOnlyKeypadButtonState("C4", values[15]),
                new ReadOnlyKeypadButtonState("C5", values[16]),
                new ReadOnlyKeypadButtonState("C6", values[17]),
                new ReadOnlyKeypadButtonState("D1", values[18]),
                new ReadOnlyKeypadButtonState("D2", values[19]),
                new ReadOnlyKeypadButtonState("D3", values[20]),
                new ReadOnlyKeypadButtonState("D4", values[21]),
                new ReadOnlyKeypadButtonState("D5", values[22]),
                new ReadOnlyKeypadButtonState("D6", values[23]),
            };

            return result;
        }

        public IEnumerable<TestCaseData>  ConvertButtonsWorksData
        {
            get
            {
                var data = new byte[] {0, 0, 0, 0, 0, 0, 0};
                var buttons = Buttons(new bool?[]
                {
                    false, false, false, false, false, false, false, false, false, false, false, false,
                    false, false, false, false, false, false, false, false, false, false, false, false,
                });
                yield return new TestCaseData(data, buttons)
                    .SetDescription("none pressed");

                data = new byte[] { 0, 0, 0, 1, 0, 0, 0 };
                buttons = Buttons(new bool?[]
                {
                    true, false, false, false, false, false, false, false, false, false, false, false,
                    false, false, false, false, false, false, false, false, false, false, false, false,
                });
                yield return new TestCaseData(data, buttons)
                    .SetName("one pressed");

                for (var i = 0; i < 24; i++)
                {
                    data = new byte[] { 0, 0, 0, 0, 0, 0, 0 };
                    var values = new bool?[]
                    {
                        false, false, false, false, false, false, false, false, false, false, false, false,
                        false, false, false, false, false, false, false, false, false, false, false, false,
                    };
                    
                    var index = 3 + (i / 6);
                    var mask = (byte) Math.Pow(2, i%6);
                    data[index] = mask;

                    values[i] = true;

                    buttons = Buttons(values);

                    yield return new TestCaseData(data, buttons)
                        .SetName("Button " + i + " pressed");
                }

                data = new byte[] { 0, 0, 0, 63, 63, 63, 63 };
                buttons = Buttons(new bool?[]
                {
                    true, true, true, true, true, true, true, true, true, true, true, true,
                    true, true, true, true, true, true, true, true, true, true, true, true,
                });
                yield return new TestCaseData(data, buttons)
                    .SetName("all pressed");
            }
        }
    }
}
