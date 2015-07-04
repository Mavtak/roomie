using System;
using System.Text.RegularExpressions;

namespace Roomie.Common.Measurements.Ratio
{
    public static class RatioParser
    {
        private const string Pattern = @"^(?<value>\d+([.]\d+)?" + OptionalScientificNotationPattern + @") [ ]* (?<type>(%|percent))$";
        private const string OptionalScientificNotationPattern = MeasurementParser.OptionalScientificNotationPattern;

        private static readonly Regex PatternRegex = new Regex(Pattern, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        public static bool IsRatio(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            return PatternRegex.IsMatch(input);
        }

        public static IRatio Parse(string input)
        {
            var match = PatternRegex.Match(input);

            if (!match.Success)
            {
                throw new ArgumentException("The input does not represent a valid ratio", "input");
            }

            var value = Convert.ToDouble(match.Groups["value"].Value);
            var type = match.Groups["type"].Value;
            var result = Parse(value, type);

            return result;
        }

        public static IRatio Parse(double value, string type)
        {
            IRatio result;

            switch (type.ToLower())
            {
                case "%":
                case "percent":
                    result = new PercentageRatio(value);
                    break;

                default:
                    throw new Exception("Could not determine type " + type);
            }

            return result;
        }

        public static IRatio ToRatio(this string input)
        {
            return Parse(input);
        }
    }
}
