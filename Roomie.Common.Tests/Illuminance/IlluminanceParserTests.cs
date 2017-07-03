using System;
using NUnit.Framework;
using Roomie.Common.Measurements.Illuminance;

namespace Roomie.Common.Tests.Illuminance
{
    public class IlluminanceParserTests
    {
        public readonly string[] Valid = new[] {"0l", "0 l", "0  l", "0.1l", "0.1 l", "1E-06l", "1.23E+04l"};
        public readonly string[] Invalid = new[] {"", " ", "0", "lux", " 0l", "0\t%", "0.%", ".0l", "1 E-06"};

        [TestCaseSource("Valid")]
        public void ItAcceptsWellFormedValues(string input)
        {
            var result = IlluminanceParser.IsIlluminance(input);

            Assert.That(result, Is.True);
        }

        [TestCaseSource("Invalid")]
        public void ItRejectsPoorlyFormedValues(string input)
        {
            var result = IlluminanceParser.IsIlluminance(input);

            Assert.That(result, Is.False);
        }

        [TestCaseSource("Valid")]
        public void ItDoesNotThrowAnExceptionWhenParsingValidValues(string input)
        {
            IlluminanceParser.Parse(input);
        }

        [TestCaseSource("Invalid")]
        [ExpectedException]
        public void ItThrowsAnExceptionWhenParsingInvalidValues(string input)
        {
            IlluminanceParser.Parse(input);
        }

        [TestCase("0l", 0)]
        [TestCase("10l", 10)]
        [TestCase("123123123l", 123123123)]
        [TestCase("123.456l", 123.456)]
        [TestCase("1E-6l", 0.000001)]
        [TestCase("1.23E+4l", 12300)]
        public void ItParsesTheValueProperly(string input, double expected)
        {
            var result = IlluminanceParser.Parse(input);

            Assert.That(result.Value, Is.EqualTo(expected));
        }

        [TestCase("0l", typeof(LuxIlluminance))]
        [TestCase("0lux", typeof(LuxIlluminance))]
        [TestCase("0Lux", typeof(LuxIlluminance))]
        [TestCase("0E-06l", typeof(LuxIlluminance))]
        public void ItParsesTheTypeProperly(string input, Type type)
        {
            var result = IlluminanceParser.Parse(input);

            Assert.That(result.GetType(), Is.EqualTo(type));
        }
    }
}