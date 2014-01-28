using System;
using NUnit.Framework;
using Roomie.Common.Measurements.Temperature;

namespace Roomie.Common.Tests.Temperature
{
    public class TemperatureParserTests
    {
        public readonly string[] ValidTemperatures = new[] {"0C", "0 C", "0  C", "0.1C", "0.1 C"};
        public readonly string[] InvalidTemperatures = new[] {"", " ", "0", "kelvin", " 0C", "0\tC", "0.C", ".0C"};

        [TestCase("0C")]
        [TestCase("0 C")]
        [TestCase("0  C")]
        [TestCase("0.1C")]
        public void ItAcceptsWellFormedTemperatures(string input)
        {
            var result = TemperatureParser.IsTemperature(input);

            Assert.That(result, Is.True);
        }

        [TestCaseSource("InvalidTemperatures")]
        public void ItRejectsPoorlyFormedTemperatures(string input)
        {
            var result = TemperatureParser.IsTemperature(input);

            Assert.That(result, Is.False);
        }

        [TestCaseSource("ValidTemperatures")]
        public void ItDoesNotThrowAnExceptionWhenParsingValidTemperatures(string input)
        {
            TemperatureParser.Parse(input);
        }

        [TestCaseSource("InvalidTemperatures")]
        [ExpectedException]
        public void ItThrowsAnExceptionWhenParsingInvalidTemperatures(string input)
        {
            TemperatureParser.Parse(input);
        }

        [TestCase("0C", 0)]
        [TestCase("10C", 10)]
        [TestCase("123123123C", 123123123)]
        [TestCase("123.456C", 123.456)]
        public void ItParsesTheValueProperly(string input, double expected)
        {
            var result = TemperatureParser.Parse(input);

            Assert.That(result.Value, Is.EqualTo(expected));
        }

        [TestCase("0C", typeof (CelsiusTemperature))]
        [TestCase("0c", typeof (CelsiusTemperature))]
        [TestCase("0Celsius", typeof (CelsiusTemperature))]
        [TestCase("0celsius", typeof (CelsiusTemperature))]
        [TestCase("0F", typeof (FahrenheitTemperature))]
        [TestCase("0f", typeof (FahrenheitTemperature))]
        [TestCase("0Fahrenheit", typeof (FahrenheitTemperature))]
        [TestCase("0fahrenheit", typeof (FahrenheitTemperature))]
        [TestCase("0K", typeof (KelvinTemperature))]
        [TestCase("0k", typeof (KelvinTemperature))]
        [TestCase("0Kelvin", typeof (KelvinTemperature))]
        [TestCase("0kelvin", typeof (KelvinTemperature))]
        public void ItParsesTheTypeProperly(string input, Type type)
        {
            var result = TemperatureParser.Parse(input);

            Assert.That(result.GetType(), Is.EqualTo(type));
        }
    }
}