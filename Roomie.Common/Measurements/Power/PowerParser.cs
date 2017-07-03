using System;
using System.Text.RegularExpressions;

namespace Roomie.Common.Measurements.Power
{
    public static class PowerParser
    {
        private const string Pattern = @"^(?<value>\d+([.]\d+)?" + OptionalScientificNotationPattern + @") [ ]* (?<type>(%|w|watt|watts))$";
        private const string OptionalScientificNotationPattern = MeasurementParser.OptionalScientificNotationPattern;

        private static readonly Regex PatternRegex = new Regex(Pattern, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        public static bool IsPower(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            return PatternRegex.IsMatch(input);
        }

        public static IPower Parse(string input)
        {
            var match = PatternRegex.Match(input);

            if (!match.Success)
            {
                throw new ArgumentException("The input does not represent a valid Power", "input");
            }

            var value = Convert.ToDouble(match.Groups["value"].Value);
            var type = match.Groups["type"].Value;
            var result = Parse(value, type);

            return result;
        }

        public static IPower Parse(double value, string type)
        {
            IPower result;

            switch (type.ToLower())
            {
                case "w":
                case "watt":
                case "watts":
                    result = new WattsPower(value);
                    break;

                default:
                    throw new Exception("Could not determine type " + type);
            }

            return result;
        }

        public static IPower ToPower(this string input)
        {
            return Parse(input);
        }
    }
}
