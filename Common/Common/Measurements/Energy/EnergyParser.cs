using System;
using System.Text.RegularExpressions;

namespace Roomie.Common.Measurements.Energy
{
    public static class EnergyParser
    {
        private const string Pattern = @"^(?<value>\d+([.]\d+)?" + OptionalScientificNotationPattern + @") [ ]* (?<type>(j|joule|joules|kwh|kilowatt[ ]hour|kilowatt[ ]hours))$";
        private const string OptionalScientificNotationPattern = MeasurementParser.OptionalScientificNotationPattern;

        private static readonly Regex PatternRegex = new Regex(Pattern, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        public static bool IsEnergy(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            return PatternRegex.IsMatch(input);
        }

        public static IEnergy Parse(string input)
        {
            var match = PatternRegex.Match(input);

            if (!match.Success)
            {
                throw new ArgumentException("The input does not represent a valid Energy", "input");
            }

            var value = Convert.ToDouble(match.Groups["value"].Value);
            var type = match.Groups["type"].Value;
            var result = Parse(value, type);

            return result;
        }

        public static IEnergy Parse(double value, string type)
        {
            IEnergy result;

            switch (type.ToLower())
            {
                case "j":
                case "joule":
                case "joules":
                    result = new JoulesEnergy(value);
                    break;

                case "kwh":
                case "kilowatt hour":
                case "kilowatt hours":
                    result = new KilowattHoursEnergy(value);
                    break;

                default:
                    throw new Exception("Could not determine type " + type);
            }

            return result;
        }

        public static IEnergy ToEnergy(this string input)
        {
            return Parse(input);
        }
    }
}
