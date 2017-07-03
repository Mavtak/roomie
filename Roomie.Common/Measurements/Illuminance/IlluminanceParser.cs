using System;
using System.Text.RegularExpressions;

namespace Roomie.Common.Measurements.Illuminance
{
    public static class IlluminanceParser
    {
        private const string Pattern = @"^(?<value>\d+([.]\d+)?" + OptionalScientificNotationPattern + @") [ ]* (?<type>(l|lux))$";
        private const string OptionalScientificNotationPattern = MeasurementParser.OptionalScientificNotationPattern;

        private static readonly Regex PatternRegex = new Regex(Pattern, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        public static bool IsIlluminance(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            return PatternRegex.IsMatch(input);
        }

        public static IIlluminance Parse(string input)
        {
            var match = PatternRegex.Match(input);

            if (!match.Success)
            {
                throw new ArgumentException("The input does not represent a valid Illuminance", "input");
            }

            var value = Convert.ToDouble(match.Groups["value"].Value);
            var type = match.Groups["type"].Value;
            var result = Parse(value, type);

            return result;
        }

        public static IIlluminance Parse(double value, string type)
        {
            IIlluminance result;

            switch (type.ToLower())
            {
                case "l":
                case "lux":
                    result = new LuxIlluminance(value);
                    break;

                default:
                    throw new Exception("Could not determine type " + type);
            }

            return result;
        }

        public static IIlluminance ToIlluminance(this string input)
        {
            return Parse(input);
        }
    }
}
