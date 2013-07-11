using NUnit.Framework;
using Roomie.Common.Temperature;

namespace Roomie.Common.Tests.Temperature
{
    [TestFixture]
    public class FahrenheitTemperatureTests
    {
        [TestCase(0, -17.7778)]
        [TestCase(100, 37.7778)]
        public void ItShouldConvertToCelsiustProperly(double before, double after)
        {
            var fahrenheit = new FahrenheitTemperature(before);
            var celsius = fahrenheit.Celsius;

            Assert.That(celsius.Value, Is.EqualTo(after).Within(0.001));
        }

        [Test]
        public void ItShouldReturnItselfForFahrenheit()
        {
            var fahrenheit = new FahrenheitTemperature(0);
            var newfahrenheit = fahrenheit.Fahrenheit;

            Assert.That(fahrenheit, Is.SameAs(newfahrenheit));
        }

        [TestCase(0, 255.372)]
        [TestCase(100, 310.928)]
        public void ItShouldConvertToKelvinProperly(double before, double after)
        {
            var fahrenheit = new FahrenheitTemperature(before);
            var kelvin = fahrenheit.Kelvin;

            Assert.That(kelvin.Value, Is.EqualTo(after).Within(0.001));
        }

        [TestCase(100, "100 Fahrenheit")]
        public void ToStringShouldWork(double value, string expected)
        {
            var fahrenheit = new FahrenheitTemperature(value);
            var actual = fahrenheit.ToString();

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
