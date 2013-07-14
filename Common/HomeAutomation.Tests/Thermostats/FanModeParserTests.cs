using NUnit.Framework;
using Roomie.Common.HomeAutomation.Thermostats.Fans;

namespace Roomie.Common.HomeAutomation.Tests.Thermostats
{
    public class ThermostatFanModeParserTests
    {
        public string[] ValidInputs = new[] {"Auto", "auto", "On", "on"};
        public string[] InvalidInputs = new[] {"off", "fast", "", " ", "auto ", " auto"};

        [TestCaseSource("ValidInputs")]
        public void ItAcceptsWellFormedInput(string input)
        {
            var result = ThermostatFanModeParser.IsValid(input);

            Assert.That(result, Is.True);
        }

        [TestCaseSource("InvalidInputs")]
        public void ItRejectsMalformedInput(string input)
        {
            var result = ThermostatFanModeParser.IsValid(input);

            Assert.That(result, Is.False);
        }

        [TestCaseSource("ValidInputs")]
        public void ItDoesNotThrowExceptionsWhenParsingValidInput(string input)
        {
            ThermostatFanModeParser.Parse(input);
        }

        [TestCaseSource("InvalidInputs")]
        [ExpectedException]
        public void ItThrowsExceptionsWhenParsingValidInput(string input)
        {
            ThermostatFanModeParser.Parse(input);
        }

        [TestCase("Auto", ThermostatFanMode.Auto)]
        [TestCase("auto", ThermostatFanMode.Auto)]
        [TestCase("On", ThermostatFanMode.On)]
        [TestCase("on", ThermostatFanMode.On)]
        public void ItParsesValuesProperly(string input, ThermostatFanMode expected)
        {
            var actual = ThermostatFanModeParser.Parse(input);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
