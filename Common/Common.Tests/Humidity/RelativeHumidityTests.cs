using NUnit.Framework;
using Roomie.Common.Measurements.Humidity;

namespace Roomie.Common.Tests.Humidity
{
    public class RelativeHumidityTests
    {
        [TestCase(100, "100%")]
        public void ToStringShouldWork(double value, string expected)
        {
            var original = new RelativeHumidity(value);
            var actual = original.ToString();

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
