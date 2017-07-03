using NUnit.Framework;
using Roomie.Common.Measurements.Ratio;

namespace Roomie.Common.Tests.Ratio
{
    public class PercentageRatioTests
    {
        [TestCase(100, "100%")]
        public void ToStringShouldWork(double value, string expected)
        {
            var original = new PercentageRatio(value);
            var actual = original.ToString();

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
