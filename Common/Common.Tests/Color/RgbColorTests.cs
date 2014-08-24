using NUnit.Framework;
using Roomie.Common.Color;

namespace Roomie.Common.Tests.Color
{
    public class RgbColorTests
    {
        [Test]
        public void ItSavesValuesCorrectly()
        {
            var color = new RgbColor(1, 2, 3);

            Assert.That(color.Red, Is.EqualTo(1));
            Assert.That(color.Green, Is.EqualTo(2));
            Assert.That(color.Blue, Is.EqualTo(3));
        }

        [Test]
        public void RgbVersionIsItself()
        {
            var original = new RgbColor(1, 2, 3);
            var copy = original.RedGreenBlue;

            Assert.That(original, Is.SameAs(copy));
        }

        [TestCase(255, 0, 0, "red")]
        [TestCase(0, 255, 255, "aqua")]
        [TestCase(1, 2, 3, null)]
        public void NamedVersionWorks(byte red, byte green, byte blue, string nameValue)
        {
            var rgb = new RgbColor(red, green, blue);
            var name = rgb.Name;

            Assert.That(name.Value, Is.EqualTo(nameValue));
        }
    }
}
