using System;
using NUnit.Framework;
using Roomie.Common.Measurements.Ratio;

namespace Roomie.Common.Tests.Ratio
{
    public class RatioParserTests
    {
        public readonly string[] Valid = new[] {"0%", "0 %", "0  %", "0.1%", "0.1 %", "1E-06%", "1.23E+04%"};
        public readonly string[] Invalid = new[] {"", " ", "0", "percent", " 0%", "0\t%", "0.%", ".0%", "1 E-06"};

        [TestCaseSource("Valid")]
        public void ItAcceptsWellFormedValues(string input)
        {
            var result = RatioParser.IsRatio(input);

            Assert.That(result, Is.True);
        }

        [TestCaseSource("Invalid")]
        public void ItRejectsPoorlyFormedValues(string input)
        {
            var result = RatioParser.IsRatio(input);

            Assert.That(result, Is.False);
        }

        [TestCaseSource("Valid")]
        public void ItDoesNotThrowAnExceptionWhenParsingValidValues(string input)
        {
            RatioParser.Parse(input);
        }

        [TestCaseSource("Invalid")]
        [ExpectedException]
        public void ItThrowsAnExceptionWhenParsingInvalidValues(string input)
        {
            RatioParser.Parse(input);
        }

        [TestCase("0%", 0)]
        [TestCase("10%", 10)]
        [TestCase("123123123%", 123123123)]
        [TestCase("123.456%", 123.456)]
        [TestCase("1E-6%", 0.000001)]
        [TestCase("1.23E+4%", 12300)]
        public void ItParsesTheValueProperly(string input, double expected)
        {
            var result = RatioParser.Parse(input);

            Assert.That(result.Value, Is.EqualTo(expected));
        }

        [TestCase("0%", typeof(PercentageRatio))]
        [TestCase("0percent", typeof(PercentageRatio))]
        [TestCase("0Percent", typeof(PercentageRatio))]
        [TestCase("0E-06%", typeof(PercentageRatio))]
        public void ItParsesTheTypeProperly(string input, Type type)
        {
            var result = RatioParser.Parse(input);

            Assert.That(result.GetType(), Is.EqualTo(type));
        }
    }
}