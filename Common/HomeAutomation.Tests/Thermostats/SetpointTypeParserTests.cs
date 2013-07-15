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
            var result = SetpointTypeParser.IsValid(input);

            Assert.That(result, Is.True);
        }

        [TestCaseSource("InvalidInputs")]
        public void ItRejectsMalformedInput(string input)
        {
            var result = SetpointTypeParser.IsValid(input);

            Assert.That(result, Is.False);
        }

        [TestCaseSource("ValidInputs")]
        public void ItDoesNotThrowExceptionsWhenParsingValidInput(string input)
        {
            SetpointTypeParser.Parse(input);
        }

        [TestCaseSource("InvalidInputs")]
        [ExpectedException]
        public void ItThrowsExceptionsWhenParsingValidInput(string input)
        {
            SetpointTypeParser.Parse(input);
        }

        [TestCase("Heat", SetpointType.Heat)]
        [TestCase("heat", SetpointType.Heat)]
        [TestCase("Cool", SetpointType.Cool)]
        [TestCase("cool", SetpointType.Cool)]
        public void ItParsesValuesProperly(string input, SetpointType expected)
        {
            var actual = SetpointTypeParser.Parse(input);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
