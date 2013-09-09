using NUnit.Framework;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;

namespace Roomie.Common.HomeAutomation.Tests.Thermostats
{
    public class SetpointTypeParserTests
    {
        public string[] ValidInputs = new[] {"Heat", "Cool", "heat", "cool"};
        public string[] InvalidInputs = new[] {"hot", "cold", "", " ", "heat ", " cool"};

        [TestCaseSource("ValidInputs")]
        public void ItAcceptsWellFormedInput(string input)
        {
            var result = ThermostatSetpointTypeParser.IsValid(input);

            Assert.That(result, Is.True);
        }

        [TestCaseSource("InvalidInputs")]
        public void ItRejectsMalformedInput(string input)
        {
            var result = ThermostatSetpointTypeParser.IsValid(input);

            Assert.That(result, Is.False);
        }

        [TestCaseSource("ValidInputs")]
        public void ItDoesNotThrowExceptionsWhenParsingValidInput(string input)
        {
            ThermostatSetpointTypeParser.Parse(input);
        }

        [TestCaseSource("InvalidInputs")]
        [ExpectedException]
        public void ItThrowsExceptionsWhenParsingValidInput(string input)
        {
            ThermostatSetpointTypeParser.Parse(input);
        }

        [TestCase("Heat", ThermostatSetpointType.Heat)]
        [TestCase("heat", ThermostatSetpointType.Heat)]
        [TestCase("Cool", ThermostatSetpointType.Cool)]
        [TestCase("cool", ThermostatSetpointType.Cool)]
        public void ItParsesValuesProperly(string input, ThermostatSetpointType expected)
        {
            var actual = ThermostatSetpointTypeParser.Parse(input);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
