﻿using System;
using NUnit.Framework;
using Roomie.Common.Measurements;
using Roomie.Common.Measurements.Energy;
using Roomie.Common.Measurements.Humidity;
using Roomie.Common.Measurements.Power;
using Roomie.Common.Measurements.Ratio;
using Roomie.Common.Measurements.Temperature;

namespace Roomie.Common.Tests.Measurements
{
    public class MeasurementParserTests
    {
        [TestCase("1J", 1, typeof(JoulesEnergy))]
        public void ItParsesEnergyMeasurementsCorrectly(string input, double value, Type type)
        {
            var result = MeasurementParser.Parse<IEnergy>(input);

            Assert.That(result.Value, Is.EqualTo(value));
            Assert.That(result.GetType(), Is.EqualTo(type));
        }

        [TestCase("1W", 1, typeof(WattsPower))]
        public void ItParsesPowerMeasurementsCorrectly(string input, double value, Type type)
        {
            var result = MeasurementParser.Parse<IPower>(input);

            Assert.That(result.Value, Is.EqualTo(value));
            Assert.That(result.GetType(), Is.EqualTo(type));
        }

        [TestCase("1%", 1, typeof(PercentageRatio))]
        public void ItParsesRatioMeasurementsCorrectly(string input, double value, Type type)
        {
            var result = MeasurementParser.Parse<IRatio>(input);

            Assert.That(result.Value, Is.EqualTo(value));
            Assert.That(result.GetType(), Is.EqualTo(type));
        }

        [TestCase("1%", 1, typeof(RelativeHumidity))]
        public void ItParsesHumidityMeasurementsCorrectly(string input, double value, Type type)
        {
            var result = MeasurementParser.Parse<IHumidity>(input);

            Assert.That(result.Value, Is.EqualTo(value));
            Assert.That(result.GetType(), Is.EqualTo(type));
        }

        [TestCase("1K", 1, typeof(KelvinTemperature))]
        public void ItParsesTemperatureMeasurementsCorrectly(string input, double value, Type type)
        {
            var result = MeasurementParser.Parse<ITemperature>(input);

            Assert.That(result.Value, Is.EqualTo(value));
            Assert.That(result.GetType(), Is.EqualTo(type));
        }

        [TestCase("1Unit", 1, typeof(ReadOnlyMeasurement))]
        public void ItParsesGeneralMeasurementsCorrectly(string input, double value, Type type)
        {
            var result = MeasurementParser.Parse<IMeasurement>(input);

            Assert.That(result.Value, Is.EqualTo(value));
            Assert.That(result.GetType(), Is.EqualTo(type));
        }

        [TestCase("1E-04Unit", 0.0001, typeof(ReadOnlyMeasurement))]
        [TestCase("1.23E+5Unit", 123000, typeof(ReadOnlyMeasurement))]
        public void ItParsesScientificNotationCorrectly(string input, double value, Type type)
        {
            var result = MeasurementParser.Parse<IMeasurement>(input);

            Assert.That(result.Value, Is.EqualTo(value));
            Assert.That(result.GetType(), Is.EqualTo(type));
        }
    }
}
