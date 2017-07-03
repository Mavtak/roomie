using NUnit.Framework;
using Roomie.Common.Measurements.Power;

namespace Roomie.Common.Tests.Power
{
    public class WattsPowerTests
    {
        [TestCase(100, "100 Watts")]
        public void ToStringShouldWork(double value, string expected)
        {
            var original = new WattsPower(value);
            var actual = original.ToString();

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
