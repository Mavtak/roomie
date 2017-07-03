using NUnit.Framework;

namespace Roomie.Common.HomeAutomation.Tests
{
    [TestFixture]
    public class LocationExtensionTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("part1")]
        [TestCase("part1/part2")]
        [TestCase("part1/part2/part3")]
        public void FormatWorks(string format)
        {
            var parts = new Location(format);
            var newFormat = parts.Format();

            Assert.That(format??string.Empty, Is.EqualTo(newFormat));
        }


        [TestCase("Inside/Upstairs/Living Room/Aquarium/Thermostat", "Inside/Thermostat", 3)]
        [TestCase("Inside/Upstairs/Living Room/Aquarium/Thermostat", "Inside/Upstairs/Thermostat", 2)]
        [TestCase("Inside/Upstairs/Living Room/Aquarium/Thermostat", "Aquarium/Thermostat", 3)]
        [TestCase("Inside/Upstairs/Living Room/Aquarium/Thermostat", "Living Room/Thermostat", 3)]
        [TestCase("Inside/Upstairs/Living Room/Aquarium/Thermostat", "Upstairs/Thermostat", 3)]
        [TestCase("Inside/Upstairs/Living Room/Aquarium/Thermostat", "Outside", null)]

        [TestCase("Inside/Upstairs/Thermostat", "Inside/Thermostat", 1)]
        [TestCase("Inside/Upstairs/Thermostat", "Inside/Upstairs/Thermostat", 0)]
        [TestCase("Inside/Upstairs/Thermostat", "Aquarium/Thermostat", null)]
        [TestCase("Inside/Upstairs/Thermostat", "Living Room/Thermostat", null)]
        [TestCase("Inside/Upstairs/Thermostat", "Upstairs/Thermostat", 1)]
        [TestCase("Inside/Upstairs/Thermostat", "Outside", null)]

        [TestCase("Inside/Downstairs/Thermostat", "Inside/Thermostat", 1)]
        [TestCase("Inside/Downstairs/Thermostat", "Inside/Upstairs/Thermostat", null)]
        [TestCase("Inside/Downstairs/Thermostat", "Aquarium/Thermostat", null)]
        [TestCase("Inside/Downstairs/Thermostat", "Living Room/Thermostat", null)]
        [TestCase("Inside/Downstairs/Thermostat", "Upstairs/Thermostat", null)]
        [TestCase("Inside/Downstairs/Thermostat", "Outside", null)]

        [TestCase("Inside/Upstairs/Thermostat", "Upstairs/Master Bedroom/Master Bathroom/Thermostat", 3)]
        [TestCase("Derp", "derp", 0)]

        [TestCase("a/b/c", "", 3)]

        public void CalculateClosenessWorks(string locationAFormat, string locationBFormat, int? expected)
        {
            var locationA = new Location(locationAFormat);
            var locationB = new Location(locationBFormat);

            var actual = locationA.CalculateCloseness(locationB);

            Assert.That(actual, Is.EqualTo(expected));

            var inverse = locationB.CalculateCloseness(locationA);
            
            Assert.That(inverse, Is.EqualTo(expected));
        }
    }
}
