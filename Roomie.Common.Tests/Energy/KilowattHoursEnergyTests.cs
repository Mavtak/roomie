using NUnit.Framework;
using Roomie.Common.Measurements.Energy;

namespace Roomie.Common.Tests.Energy
{
    [TestFixture]
    public class KilowattHoursEnergyTests
    {
        [Test]
        public void ItShouldReturnItselfForKilowattHours()
        {
            var original = new KilowattHoursEnergy(0);
            var conversion = original.KilowattHours;

            Assert.That(original, Is.SameAs(conversion));
        }

        [TestCase(0, 0)]
        [TestCase(100, 360000000)]
        public void ItShouldConvertToJoulesProperly(double before, double after)
        {
            var original = new KilowattHoursEnergy(before);
            var conversion = original.Joules;

            Assert.That(conversion.Value, Is.EqualTo(after).Within(0.001));
        }

        [TestCase(100, "100 Kilowatt Hours")]
        public void ToStringShouldWork(double value, string expected)
        {
            var original = new KilowattHoursEnergy(value);
            var actual = original.ToString();

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
