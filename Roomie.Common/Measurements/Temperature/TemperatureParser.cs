using System;
using System.Text.RegularExpressions;

namespace Roomie.Common.Measurements.Temperature
{
    public static class TemperatureParser
    {
        //TODO: update other measurements to allow for scientific notation
        private const string Pattern = @"^(?<value>\d+([.]\d+)?" + OptionalScientificNotationPattern + @") [ ]* (?<type>(c|f|k|celsius|fahrenheit|kelvin))$";
        private const string OptionalScientificNotationPattern = MeasurementParser.OptionalScientificNotationPattern;

        private static readonly Regex PatternRegex = new Regex(Pattern, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        public static bool IsTemperature(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            return PatternRegex.IsMatch(input);
        }

        public static ITemperature Parse(string input)
        {
            var match = PatternRegex.Match(input);

            if (!match.Success)
            {
                throw new ArgumentException("The input does not represent a valid temperature", "input");
            }

            var value = Convert.ToDouble(match.Groups["value"].Value);
            var type = match.Groups["type"].Value;
            var result = Parse(value, type);

            return result;
        }

        public static ITemperature Parse(double value, string type)
        {
            ITemperature result;

            switch (type.ToLower())
            {
                case "c":
                case "celsius":
                    result = new CelsiusTemperature(value);
                    break;

                case "f":
                case "fahrenheit":
                    result = new FahrenheitTemperature(value);
                    break;

                case "k":
                case "kelvin":
                    result = new KelvinTemperature(value);
                    break;

                default:
                    throw new Exception("Could not determine type " + type);
            }

            return result;
        }

        public static ITemperature ToTemperature(this string input)
        {
            return Parse(input);
        }
    }
}
