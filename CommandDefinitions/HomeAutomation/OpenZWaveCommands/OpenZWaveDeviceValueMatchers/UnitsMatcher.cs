using System;
using System.Linq;

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

        public static IOpenZWaveDeviceValueMatcher Luminance()
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
