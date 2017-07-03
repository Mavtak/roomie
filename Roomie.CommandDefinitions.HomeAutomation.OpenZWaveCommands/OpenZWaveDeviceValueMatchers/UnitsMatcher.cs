using System;
using System.Linq;
using System.Text;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.OpenZWaveDeviceValueMatchers
{
    public class UnitsMatcher : IOpenZWaveDeviceValueMatcher
    {
        private readonly string[] _units;

        public UnitsMatcher(params string[] units)
        {
            _units = units;
        }

        public bool Matches(OpenZWaveDeviceValue entry)
        {
            var result = _units.Any(x => string.Equals(x, entry.Units, StringComparison.InvariantCultureIgnoreCase));

            return result;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append("Units ");

            if (_units.Length == 1)
            {
                result.Append("= ");
                result.Append(_units.First());
            }
            else
            {
                result.Append("in (");
                result.Append(string.Join(",", _units));
                result.Append(")");
            }

            return result.ToString();
        }

        public static IOpenZWaveDeviceValueMatcher Time()
        {
            var result = new UnitsMatcher("hours", "minutes", "seconds", "milliseconds");

            return result;
        }

        public static IOpenZWaveDeviceValueMatcher Temperature()
        {
            var result = new UnitsMatcher("C", "F");

            return result;
        }

        public static IOpenZWaveDeviceValueMatcher Illuminance()
        {
            var result = new UnitsMatcher("lux");

            return result;
        }

        public static IOpenZWaveDeviceValueMatcher Humidity()
        {
            var result = new UnitsMatcher("%");

            return result;
        }
    }
}
