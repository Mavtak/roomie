using System;
using NUnit.Framework;
using Roomie.Common.Measurements.Humidity;

namespace Roomie.Common.Tests.Humidity
{
    public class HumidityParserTests
    {
        public readonly string[] Valid = new[] {"0%", "0 %", "0  %", "0.1%", "0.1 %"};
        public readonly string[] Invalid = new[] {"", " ", "0", "percent", " 0%", "0\t%", "0.%", ".0%"};

        [TestCaseSource("Valid")]
        public void ItAcceptsWellFormedValues(string input)
        {
            var result = HumidityParser.IsHumidity(input);

            Assert.That(result, Is.True);
        }

        [TestCaseSource("Invalid")]
        public void ItRejectsPoorlyFormedValues(string input)
        {
            var result = HumidityParser.IsHumidity(input);

            Assert.That(result, Is.False);
        }

        [TestCaseSource("Valid")]
        public void ItDoesNotThrowAnExceptionWhenParsingValidValues(string input)
        {
            HumidityParser.Parse(input);
        }

        [TestCaseSource("Invalid")]
        [ExpectedException]
        public void ItThrowsAnExceptionWhenParsingInvalidValues(string input)
        {
            HumidityParser.Parse(input);
        }

        [TestCase("0%", 0)]
        [TestCase("10%", 10)]
        [TestCase("123123123%", 123123123)]
        [TestCase("123.456%", 123.456)]
        public void ItParsesTheValueProperly(string input, double expected)
        {
            var result = HumidityParser.Parse(input);

            Assert.That(result.Value, Is.EqualTo(expected));
        }

        [TestCase("0%", typeof(RelativeHumidity))]
        [TestCase("0percent", typeof(RelativeHumidity))]
        [TestCase("0Percent", typeof(RelativeHumidity))]
        public void ItParsesTheTypeProperly(string input, Type type)
        {
            var result = HumidityParser.Parse(input);

            Assert.That(result.GetType(), Is.EqualTo(type));
        }
    }
}