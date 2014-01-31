using NUnit.Framework;
using Roomie.Common.Measurements.Energy;

namespace Roomie.Common.Tests.Energy
{
    [TestFixture]
    public class JoulesEnergyTests
    {
        [Test]
        public void ItShouldReturnItselfForJoules()
        {
            var original = new JoulesEnergy(0);
            var conversion = original.Joules;

            Assert.That(original, Is.SameAs(conversion));
        }

        [TestCase(0, 0)]
        [TestCase(100, .0000277777778)]
        public void ItShouldConvertToKilowattHoursProperly(double before, double after)
        {
            var original = new JoulesEnergy(before);
            var conversion = original.KilowattHours;

            Assert.That(conversion.Value, Is.EqualTo(after).Within(0.001));
        }

        [TestCase(100, "100 Joules")]
        public void ToStringShouldWork(double value, string expected)
        {
            var original = new JoulesEnergy(value);
            var actual = original.ToString();

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
