using System;
using NUnit.Framework;
using Roomie.Common.Color;

namespace Roomie.Common.Tests.Color
{
    public class NamedColorTests
    {
        [Test]
        public void ItSavesValuesCorrectly()
        {
            var color = new NamedColor("Derp");

            Assert.That(color.Value, Is.EqualTo("Derp"));
        }

        [Test]
        public void NamedVersionIsItself()
        {
            var original = new NamedColor("Derp");
            var copy = original.Name;

            Assert.That(original, Is.SameAs(copy));
        }

        [TestCase("Red", 255, 0, 0)]
        [TestCase("red", 255, 0, 0)]
        [TestCase("cyan", 0, 255, 255)]
        public void RgbVersionWorks(string nameValue, byte red, byte green, byte blue)
        {
            var name = new NamedColor(nameValue);
            var rgb = name.RedGreenBlue;

            Assert.That(rgb.Red, Is.EqualTo(red));
            Assert.That(rgb.Green, Is.EqualTo(green));
            Assert.That(rgb.Blue, Is.EqualTo(blue));
        }

        [TestCase("Derp")]
        [TestCase("")]
        [TestCase(null)]
        public void RgbVersionReturnsNullForInvalidNames(string nameValue)
        {
            var name = new NamedColor(nameValue);
            var rgb = name.RedGreenBlue;

            Assert.That(rgb, Is.Null);
        }
    }
}
