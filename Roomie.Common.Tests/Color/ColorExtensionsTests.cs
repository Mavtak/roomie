using System.Linq;
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

        [TestCase("#000000", "#000000")]
        [TestCase("#000000", "#000000,#000000")]
        [TestCase("#000000", "#000000,#000000,#000000")]
        [TestCase("#FFFFFF", "#FFFFFF")]
        [TestCase("#FFFFFF", "#FFFFFF,#FFFFFF")]
        [TestCase("#FFFFFF", "#FFFFFF,#FFFFFF,#FFFFFF")]
        [TestCase("#012345", "#012345")]
        [TestCase("#FFFFFF", "#FFFFFF,#FFFFFF")]
        [TestCase("#FFFFFF", "#FFFFFF,#FFFFFF,#FFFFFF")]
        [TestCase("#7F7F7F", "#000000,#FFFFFF")]
        public void MixingColorsWorks(string expectedAsHex, string colorsAsHex)
        {
            var expected = ColorExtensions.FromHexString(expectedAsHex);
            var colors = colorsAsHex.Split(',').Select(ColorExtensions.FromHexString).ToArray();

            var actual = ColorExtensions.Mix(colors);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase("#7F007F", "red,blue")]
        public void MixingColorNamesWorksToo(string expectedAsHex, string colorNames)
        {
            var expected = ColorExtensions.FromHexString(expectedAsHex);
            var colors = colorNames.Split(',').Select(x => new NamedColor(x)).Cast<IColor>().ToArray();

            var actual = ColorExtensions.Mix(colors);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(false, "", "")]
        [TestCase(false, "#000000", "#000000")]
        [TestCase(false, "#000000,#222222", "#000000,#111111,#222222")]
        [TestCase(false, "#000000,#222222,#444444", "#000000,#111111,#222222,#333333,#444444")]
        [TestCase(true, "", "")]
        [TestCase(true, "#000000", "#000000")]
        [TestCase(true, "#000000,#222222", "#000000,#111111,#222222")]
        [TestCase(true, "#000000,#222222,#444444", "#000000,#111111,#222222,#333333,#444444,#222222")]
        public void AddInBetweenColorsWorks(bool cycle, string colorsAsHex, string expectedAsHex)
        {
            var expected = expectedAsHex.Split(',').Select(ColorExtensions.FromHexString).ToArray();
            var colors = colorsAsHex.Split(',').Select(ColorExtensions.FromHexString).ToArray();
            var actual = ColorExtensions.AddInBetweenColors(colors, cycle).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
