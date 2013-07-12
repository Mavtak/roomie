using NUnit.Framework;
using Roomie.Common.HomeAutomation.Thermostats;

namespace Roomie.Common.HomeAutomation.Tests.Thermostats
{
    public class FanModeParserTests
    {
        public string[] ValidInputs = new[] {"Auto", "auto", "On", "on"};
        public string[] InvalidInputs = new[] {"off", "fast", "", " ", "auto ", " auto"};

        [TestCaseSource("ValidInputs")]
        public void ItAcceptsWellFormedInput(string input)
        {
            var result = FanModeParser.IsValid(input);

            Assert.That(result, Is.True);
        }

        [TestCaseSource("InvalidInputs")]
        public void ItRejectsMalformedInput(string input)
        {
            var result = FanModeParser.IsValid(input);

            Assert.That(result, Is.False);
        }

        [TestCaseSource("ValidInputs")]
        public void ItDoesNotThrowExceptionsWhenParsingValidInput(string input)
        {
            FanModeParser.Parse(input);
        }

        [TestCaseSource("InvalidInputs")]
        [ExpectedException]
        public void ItThrowsExceptionsWhenParsingValidInput(string input)
        {
            FanModeParser.Parse(input);
        }

        [TestCase("Auto", FanMode.Auto)]
        [TestCase("auto", FanMode.Auto)]
        [TestCase("On", FanMode.On)]
        [TestCase("on", FanMode.On)]
        public void ItParsesValuesProperly(string input, FanMode expected)
        {
            var actual = FanModeParser.Parse(input);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
