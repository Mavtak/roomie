using NUnit.Framework;
using Roomie.Common.Measurements.Illuminance;

namespace Roomie.Common.Tests.Illuminance
{
    public class LuxIlluminanceTests
    {
        [TestCase(100, "100 Lux")]
        public void ToStringShouldWork(double value, string expected)
        {
            var original = new LuxIlluminance(value);
            var actual = original.ToString();

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
