using System;
using NUnit.Framework;
using Roomie.Common.Measurements.Power;

namespace Roomie.Common.Tests.Power
{
    public class PowerParserTests
    {
        public readonly string[] Valid = new[] {"0w", "0 w", "0  w", "0.1w", "0.1 w"};
        public readonly string[] Invalid = new[] {"", " ", "0", "watts", " 0w", "0\t%", "0.%", ".0w"};

        [TestCaseSource("Valid")]
        public void ItAcceptsWellFormedValues(string input)
        {
            var result = PowerParser.IsPower(input);

            Assert.That(result, Is.True);
        }

        [TestCaseSource("Invalid")]
        public void ItRejectsPoorlyFormedValues(string input)
        {
            var result = PowerParser.IsPower(input);

            Assert.That(result, Is.False);
        }

        [TestCaseSource("Valid")]
        public void ItDoesNotThrowAnExceptionWhenParsingValidValues(string input)
        {
            PowerParser.Parse(input);
        }

        [TestCaseSource("Invalid")]
        [ExpectedException]
        public void ItThrowsAnExceptionWhenParsingInvalidValues(string input)
        {
            PowerParser.Parse(input);
        }

        [TestCase("0w", 0)]
        [TestCase("10w", 10)]
        [TestCase("123123123w", 123123123)]
        [TestCase("123.456w", 123.456)]
        public void ItParsesTheValueProperly(string input, double expected)
        {
            var result = PowerParser.Parse(input);

            Assert.That(result.Value, Is.EqualTo(expected));
        }

        [TestCase("0w", typeof(WattsPower))]
        [TestCase("0watt", typeof(WattsPower))]
        [TestCase("0Watt", typeof(WattsPower))]
        [TestCase("0watts", typeof(WattsPower))]
        [TestCase("0Watts", typeof(WattsPower))]
        public void ItParsesTheTypeProperly(string input, Type type)
        {
            var result = PowerParser.Parse(input);

            Assert.That(result.GetType(), Is.EqualTo(type));
        }
    }
}