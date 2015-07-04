using System;
using NUnit.Framework;
using Roomie.Common.Measurements.Energy;

namespace Roomie.Common.Tests.Energy
{
    public class EntergyParserTests
    {
        public readonly string[] Valid = new[] {"0J", "0 J", "0  J", "0.1J", "0.1 J", "1E-06J", "1.23E+04J"};
        public readonly string[] Invalid = new[] {"", " ", "0", "joules", " 0J", "0\tJ", "0.J", ".0J", "1 E-06"};

        [TestCaseSource("Valid")]
        public void ItAcceptsWellFormedValues(string input)
        {
            var result = EnergyParser.IsEnergy(input);

            Assert.That(result, Is.True);
        }

        [TestCaseSource("Invalid")]
        public void ItRejectsPoorlyFormedValues(string input)
        {
            var result = EnergyParser.IsEnergy(input);

            Assert.That(result, Is.False);
        }

        [TestCaseSource("Valid")]
        public void ItDoesNotThrowAnExceptionWhenParsingValidValues(string input)
        {
            EnergyParser.Parse(input);
        }

        [TestCaseSource("Invalid")]
        [ExpectedException]
        public void ItThrowsAnExceptionWhenParsingInvalidValues(string input)
        {
            EnergyParser.Parse(input);
        }

        [TestCase("0J", 0)]
        [TestCase("10J", 10)]
        [TestCase("123123123J", 123123123)]
        [TestCase("123.456J", 123.456)]
        [TestCase("1E-6J", 0.000001)]
        [TestCase("1.23E+4J", 12300)]
        public void ItParsesTheValueProperly(string input, double expected)
        {
            var result = EnergyParser.Parse(input);

            Assert.That(result.Value, Is.EqualTo(expected));
        }

        [TestCase("0j", typeof(JoulesEnergy))]
        [TestCase("0J", typeof(JoulesEnergy))]
        [TestCase("0E-06j", typeof(JoulesEnergy))]
        [TestCase("0joule", typeof(JoulesEnergy))]
        [TestCase("0Joule", typeof(JoulesEnergy))]
        [TestCase("0Joules", typeof(JoulesEnergy))]
        [TestCase("0joules", typeof(JoulesEnergy))]
        [TestCase("0kwh", typeof(KilowattHoursEnergy))]
        [TestCase("0KWH", typeof(KilowattHoursEnergy))]
        [TestCase("0kilowatt hour", typeof(KilowattHoursEnergy))]
        [TestCase("0Kilowatt Hour", typeof(KilowattHoursEnergy))]
        [TestCase("0kilowatt hours", typeof(KilowattHoursEnergy))]
        [TestCase("0Kilowatt Hours", typeof(KilowattHoursEnergy))]
        public void ItParsesTheTypeProperly(string input, Type type)
        {
            var result = EnergyParser.Parse(input);

            Assert.That(result.GetType(), Is.EqualTo(type));
        }
    }
}