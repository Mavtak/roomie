using System;
using System.Text.RegularExpressions;
using Roomie.Common.Measurements.Energy;
using Roomie.Common.Measurements.Humidity;
using Roomie.Common.Measurements.Illuminance;
using Roomie.Common.Measurements.Power;
using Roomie.Common.Measurements.Ratio;
using Roomie.Common.Measurements.Temperature;

namespace Roomie.Common.Measurements
{
    public static class MeasurementParser
    {
        private const string Pattern = @"^(?<value>\d+([.]\d+)?" + OptionalScientificNotationPattern + ") [ ]* (?<type>(.+))$";
        public const string OptionalScientificNotationPattern = @"(E[+-]\d+)?";

        private static readonly Regex PatternRegex = new Regex(Pattern,
                                                               RegexOptions.IgnoreCase |
                                                               RegexOptions.IgnorePatternWhitespace |
                                                               RegexOptions.Compiled);

        public static bool IsEnergy(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            return PatternRegex.IsMatch(input);
        }

        public static TMeasurement Parse<TMeasurement>(string input)
            where TMeasurement : IMeasurement
        {
            var match = PatternRegex.Match(input);

            if (!match.Success)
            {
                throw new ArgumentException("The input does not represent a valid Energy", "input");
            }

            var value = Convert.ToDouble(match.Groups["value"].Value);
            var type = match.Groups["type"].Value;
            var result = Parse<TMeasurement>(value, type);

            return result;
        }

        public static TMeasurement Parse<TMeasurement>(double value, string type)
            where TMeasurement : IMeasurement
        {
            var runtimeType = typeof (TMeasurement);

            if (runtimeType == typeof (IEnergy))
            {
                return (TMeasurement) EnergyParser.Parse(value, type);
            }

            if (runtimeType == typeof(IPower))
            {
                return (TMeasurement)PowerParser.Parse(value, type);
            }

            if (runtimeType == typeof (IRatio))
            {
                return (TMeasurement) RatioParser.Parse(value, type);
            }

            if (runtimeType == typeof (ITemperature))
            {
                return (TMeasurement) TemperatureParser.Parse(value, type);
            }

            if (runtimeType == typeof (IHumidity))
            {
                return (TMeasurement) HumidityParser.Parse(value, type);
            }

            if (runtimeType == typeof(IIlluminance))
            {
                return (TMeasurement)IlluminanceParser.Parse(value, type);
            }

            if (runtimeType == typeof (IMeasurement))
            {
                return (TMeasurement) (IMeasurement) new ReadOnlyMeasurement(value, type);
            }

            throw new Exception("Could not determine type " + runtimeType.Name);
        }

        public static TMeasurement ToMeasurement<TMeasurement>(this string input)
            where TMeasurement : IMeasurement
        {
            return Parse<TMeasurement>(input);
        }
    }
}
