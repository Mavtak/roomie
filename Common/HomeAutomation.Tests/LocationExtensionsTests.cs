using NUnit.Framework;

namespace Roomie.Common.HomeAutomation.Tests
{
    public class LocationExtensionsTests
    {
        [TestCase(true, null, null)]
        [TestCase(false, "a", null)]
        [TestCase(false, null, "b")]
        [TestCase(true, "a", "a")]
        [TestCase(true, "a/b", "a/b")]
        [TestCase(false, "a/b", "a/b/c")]
        [TestCase(false, "a/b/c", "a/b")]
        [TestCase(false, "a/b", "b/a")]
        public void EqualsWorks(bool expected, string a, string b)
        {
            var locationA = new Location(a);
            var locationB = new Location(b);

            var actual = LocationExtensions.Equals(locationA, locationB);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
