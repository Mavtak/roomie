using NUnit.Framework;
using Roomie.Common.Color;

namespace Roomie.Common.Tests.Color
{
    public class ColorExtensionsTests
    {
        [TestCase("#FFFFFF", 255, 255, 255)]
        [TestCase("#000000", 0, 0, 0)]
        [TestCase("#A1B2C3", 161, 178, 195)]
        [TestCase("#a1b2c3", 161, 178, 195)]
        public void FromHexStringWorks(string hexValue, byte red, byte green, byte blue)
        {
            var rgb = ColorExtensions.FromHexString(hexValue);

            Assert.That(rgb.Red, Is.EqualTo(red));
            Assert.That(rgb.Green, Is.EqualTo(green));
            Assert.That(rgb.Blue, Is.EqualTo(blue));
        }

        [TestCase("null")]
        [TestCase("000000")]
        [TestCase("null")]
        [TestCase("#FFF")]
        [TestCase("#short")]
        [TestCase("#toolong")]
        [TestCase("#6CHARS")]
        public void FromHexStringFailsWithNull(string hexValue)
        {
            var rgb = ColorExtensions.FromHexString(hexValue);

            Assert.That(rgb, Is.Null);
        }

        [TestCase("#FFFFFF", 255, 255, 255)]
        [TestCase("#000000", 0, 0, 0)]
        [TestCase("#A1B2C3", 161, 178, 195)]
        public void ToHexStringWorks(string hexValue, byte red, byte green, byte blue)
        {
            var rgb = new RgbColor(red, green, blue);

            var actual = rgb.ToHexString();

            Assert.That(actual, Is.EqualTo(hexValue));
        }
    }
}
