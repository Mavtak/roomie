using NUnit.Framework;
using Roomie.Common.Temperature;

namespace Roomie.Common.Tests.Temperature
{
    [TestFixture]
    public class CelsiusTemperatureTests
    {
        [Test]
        public void ItShouldReturnItselfForCelcius()
        {
            var celsius = new CelsiusTemperature(0);
            var newCelcius = celsius.Celsius;

            Assert.That(celsius, Is.SameAs(newCelcius));
        }

        [TestCase(0, 32)]
        [TestCase(100, 212)]
        public void ItShouldConvertToFarenheightProperly(double before, double after)
        {
            var celsius = new CelsiusTemperature(before);
            var fahrenheit = celsius.Fahrenheit;

            Assert.That(fahrenheit.Value, Is.EqualTo(after).Within(0.001));
        }

        [TestCase(0, 273.15)]
        [TestCase(100, 373.15)]
        public void ItShouldConvertToKelvinProperly(double before, double after)
        {
            var celsius = new CelsiusTemperature(before);
            var kelvin = celsius.Kelvin;

            Assert.That(kelvin.Value, Is.EqualTo(after).Within(0.001));
        }

        [TestCase(50, 1, 51)]
        [TestCase(50, -1, 49)]
        [TestCase(50, 0, 50)]
        public void ItAddsCorrectly(double before, double amount, double after)
        {
            var initial = new CelsiusTemperature(before);
            var result = initial.Add(amount);

            Assert.That(initial, Is.Not.SameAs(result));
            Assert.That(initial.Value, Is.EqualTo(before));
            Assert.That(result.Value, Is.EqualTo(after));
        }

        [TestCase(100, "100 Celsius")]
        public void ToStringShouldWork(double value, string expected)
        {
            var celsius = new CelsiusTemperature(value);
            var actual = celsius.ToString();

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
