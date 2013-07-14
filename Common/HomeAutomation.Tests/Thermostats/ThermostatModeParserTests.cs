using NUnit.Framework;
using Roomie.Common.HomeAutomation.Thermostats;

namespace Roomie.Common.HomeAutomation.Tests.Thermostats
{
    public class ThermostatModeParserTests
    {
        public string[] ValidInputs = new[] {"Auto", "auto", "off", "heat", "cool", "fanOnly"};
        public string[] InvalidInputs = new[] {"hot", "cold", "", " ", "auto ", " auto"};

        [TestCaseSource("ValidInputs")]
        public void ItAcceptsWellFormedInput(string input)
        {
            var result = ThermostatModeParser.IsValid(input);

            Assert.That(result, Is.True);
        }

        [TestCaseSource("InvalidInputs")]
        public void ItRejectsMalformedInput(string input)
        {
            var result = ThermostatModeParser.IsValid(input);

            Assert.That(result, Is.False);
        }

        [TestCaseSource("ValidInputs")]
        public void ItDoesNotThrowExceptionsWhenParsingValidInput(string input)
        {
            ThermostatModeParser.Parse(input);
        }

        [TestCaseSource("InvalidInputs")]
        [ExpectedException]
        public void ItThrowsExceptionsWhenParsingValidInput(string input)
        {
            ThermostatModeParser.Parse(input);
        }

        [TestCase("auto", ThermostatMode.Auto)]
        [TestCase("heat", ThermostatMode.Heat)]
        [TestCase("cool", ThermostatMode.Cool)]
        [TestCase("fanonly", ThermostatMode.FanOnly)]
        [TestCase("off", ThermostatMode.Off)]

        public void ItParsesValuesProperly(string input, ThermostatMode expected)
        {
            var actual = ThermostatModeParser.Parse(input);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
