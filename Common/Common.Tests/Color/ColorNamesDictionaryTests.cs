using NUnit.Framework;
using Roomie.Common.Color;

namespace Roomie.Common.Tests.Color
{
    public class ColorNamesDictionaryTests
    {
        private ColorNamesDictionary _dictionary;

        [SetUp]
        public void SetUp()
        {
            _dictionary = new ColorNamesDictionary();
        }

        [TestCase("Red", 255, 0, 0)]
        [TestCase("Orange", 255, 165, 0)]
        [TestCase("Yellow", 255, 255, 0)]
        [TestCase("Green", 0, 128, 0)]
        [TestCase("Blue", 0, 0, 255)]
        [TestCase("Purple", 128, 0, 128)]
        [TestCase("pUrPlE", 128, 0, 128)]
        public void ItConvertsNamesToColors(string name, byte red, byte green, byte blue)
        {
            var color = _dictionary.Find(name);
            var rgb = color.RedGreenBlue;

            Assert.That(rgb.Red, Is.EqualTo(red));
            Assert.That(rgb.Green, Is.EqualTo(green));
            Assert.That(rgb.Blue, Is.EqualTo(blue));
        }

        [TestCase("Derp")]
        [TestCase("")]
        [TestCase(null)]
        public void ItConvertsAnInvalidNameToNull(string name)
        {
            var color = _dictionary.Find(name);

            Assert.That(color, Is.Null);
        }

        [TestCase(255, 0, 0, "red")]
        [TestCase(0, 255, 255, "aqua", "cyan")]
        public void ItConvertsColorsToNames(byte red, byte green, byte blue, params string[] expected)
        {
            var color = new RgbColor(red, green, blue);
            var actual = _dictionary.Find(color);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(1, 2, 3)]
        public void ItConvertsAnInvalidColorToAnEmptyList(byte red, byte green, byte blue)
        {
            var color = new RgbColor(red, green, blue);
            var actual = _dictionary.Find(color);

            CollectionAssert.IsEmpty(actual);
        }
    }
}
