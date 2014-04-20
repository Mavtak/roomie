using System;
using System.Text.RegularExpressions;

namespace Roomie.Common.Measurements.Humidity
{
    public static class HumidityParser
    {
        private const string Pattern = @"^(?<value>\d+([.]\d+)?) [ ]* (?<type>(%|percent))$";

        private static readonly Regex PatternRegex = new Regex(Pattern, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        public static bool IsHumidity(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            return PatternRegex.IsMatch(input);
        }

        public static IHumidity Parse(string input)
        {
            var match = PatternRegex.Match(input);

            if (!match.Success)
            {
                throw new ArgumentException("The input does not represent a valid Humidity", "input");
            }

            var value = Convert.ToDouble(match.Groups["value"].Value);
            var type = match.Groups["type"].Value;
            var result = Parse(value, type);

            return result;
        }

        public static IHumidity Parse(double value, string type)
        {
            IHumidity result;

            switch (type.ToLower())
            {
                case "%":
                case "percent":
                    result = new RelativeHumidity(value);
                    break;

                default:
                    throw new Exception("Could not determine type " + type);
            }

            return result;
        }

        public static IHumidity ToHumidity(this string input)
        {
            return Parse(input);
        }
    }
}
