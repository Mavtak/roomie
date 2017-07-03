using NUnit.Framework;
using Roomie.Common.HomeAutomation.Events;

namespace Roomie.Common.HomeAutomation.Tests.Events
{
    public class EventParserTests
    {
        public string[] ValidInputs = new[] { "Powered Off", "Device Power Changed", "Device State Changed", "Network Connected" };
        public string[] InvalidInputs = new[] { "whatever", "", null };

        [TestCaseSource("ValidInputs")]
        public void ItAcceptsWellFormedInput(string input)
        {
            var result = EventTypeParser.IsValid(input);

            Assert.That(result, Is.True);
        }

        [TestCaseSource("InvalidInputs")]
        public void ItRejectsMalformedInput(string input)
        {
            var result = EventTypeParser.IsValid(input);

            Assert.That(result, Is.False);
        }

        [TestCaseSource("ValidInputs")]
        public void ItDoesNotThrowExceptionsWhenParsingValidInput(string input)
        {
            EventTypeParser.Parse(input);
        }

        [TestCaseSource("InvalidInputs")]
        [ExpectedException]
        public void ItThrowsExceptionsWhenParsingValidInput(string input)
        {
            EventTypeParser.Parse(input);
        }

        [Test]
        public void ItParsesValuesProperly()
        {
            var expected = new DevicePowerChanged();
            var actual = EventTypeParser.Parse("Device Power Changed");

            Assert.That(actual.GetType(), Is.EqualTo(expected.GetType()));
        }
    }
}
