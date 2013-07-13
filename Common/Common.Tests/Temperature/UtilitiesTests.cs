using System;
using System.Collections.Generic;
using NUnit.Framework;
using Roomie.Common.Temperature;

namespace Roomie.Common.Tests.Temperature
{
    class UtilitiesTests
    {
        public static IEnumerable<Tuple<ITemperature, ITemperature>> TrueExamples
        {
            get
            {
                yield return new Tuple<ITemperature, ITemperature>(null, null);
                yield return new Tuple<ITemperature, ITemperature>(new CelsiusTemperature(0), new FahrenheitTemperature(32));
                yield return new Tuple<ITemperature, ITemperature>(new CelsiusTemperature(0), new CelsiusTemperature(0));
            }
        }

        public static IEnumerable<Tuple<ITemperature, object>> TrueObjectExamples
        {
            get
            {
                foreach (var tuple in TrueExamples)
                {
                    yield return new Tuple<ITemperature, object>(tuple.Item1, tuple.Item2);
                }
            }
        }

        public static IEnumerable<Tuple<ITemperature, ITemperature>> FalseExamples
        {
            get
            {
                yield return new Tuple<ITemperature, ITemperature>(null, new FahrenheitTemperature(32));
                yield return new Tuple<ITemperature, ITemperature>(new CelsiusTemperature(0), null);
                yield return new Tuple<ITemperature, ITemperature>(new CelsiusTemperature(0), new CelsiusTemperature(1));
                yield return new Tuple<ITemperature, ITemperature>(new FahrenheitTemperature(32), new CelsiusTemperature(1));
            }
        }

        public static IEnumerable<Tuple<ITemperature, object>> FalseObjectExamples
        {
            get
            {
                foreach (var tuple in FalseExamples)
                {
                    yield return new Tuple<ITemperature, object>(tuple.Item1, tuple.Item2);
                }

                yield return new Tuple<ITemperature, object>(null, "hi!");
                yield return new Tuple<ITemperature, object>(new CelsiusTemperature(0), "I'm a string!");
                yield return new Tuple<ITemperature, object>(new CelsiusTemperature(0), null);
            }
        }

        [TestCaseSource("TrueExamples")]
        public static void TheseReturnTrue(Tuple<ITemperature, ITemperature> input)
        {
            var result = Utilities.EqualsHelper(input.Item1, input.Item2);

            Assert.That(result, Is.True);
        }

        [TestCaseSource("FalseExamples")]
        public static void TheseReturnFalse(Tuple<ITemperature, ITemperature> input)
        {
            var result = Utilities.EqualsHelper(input.Item1, input.Item2);

            Assert.That(result, Is.False);
        }

        [TestCaseSource("TrueObjectExamples")]
        public static void TheseAlsoReturnTrue(Tuple<ITemperature, object> input)
        {
            var result = Utilities.EqualsHelper(input.Item1, input.Item2);

            Assert.That(result, Is.True);
        }

        [TestCaseSource("FalseObjectExamples")]
        public static void TheseAlsoReturnFalse(Tuple<ITemperature, object> input)
        {
            var result = Utilities.EqualsHelper(input.Item1, input.Item2);

            Assert.That(result, Is.False);
        }
    }
}
