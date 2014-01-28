using NUnit.Framework;
using Roomie.Common.Measurements.Temperature;

namespace Roomie.Common.Tests.Temperature
{
    [TestFixture]
    public class KelvinTemperatureTests
    {
        [TestCase(0, -273.15)]
        [TestCase(100, -173.15)]
        public void ItShouldConvertToCelsiustProperly(double before, double after)
        {
            var kelvin = new KelvinTemperature(before);
            var celsius = kelvin.Celsius;

            Assert.That(celsius.Value, Is.EqualTo(after).Within(0.001));
        }

        [TestCase(0, -459.67)]
        [TestCase(100, -279.67)]
        public void ItShouldConvertToFarenheightProperly(double before, double after)
        {
            var celsius = new KelvinTemperature(before);
            var fahrenheit = celsius.Fahrenheit;

            Assert.That(fahrenheit.Value, Is.EqualTo(after).Within(0.001));
        }

        [Test]
        public void ItShouldReturnItselfForKelvin()
        {
            var kelvin = new KelvinTemperature(0);
            var newKelvin = kelvin.Kelvin;

            Assert.That(kelvin, Is.SameAs(newKelvin));
        }

        [TestCase(50, 1, 51)]
        [TestCase(50, -1, 49)]
        [TestCase(50, 0, 50)]
        public void ItAddsCorrectly(double before, double amount, double after)
        {
            var initial = new KelvinTemperature(before);
            var result = initial.Add(amount);

            Assert.That(initial, Is.Not.SameAs(result));
            Assert.That(initial.Value, Is.EqualTo(before));
            Assert.That(result.Value, Is.EqualTo(after));
        }

        [TestCase(100, "100 Kelvin")]
        public void ToStringShouldWork(double value, string expected)
        {
            var kelvin = new KelvinTemperature(value);
            var actual = kelvin.ToString();

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
