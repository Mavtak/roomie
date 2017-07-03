using System.Collections.Generic;
using System.Linq;
using Roomie.CommandDefinitions.OpenZWaveCommands.OpenZWaveDeviceValueMatchers;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public static class OpenZWaveDeviceValueExtensions
    {
        public static IEnumerable<OpenZWaveDeviceValue> Matches(this IEnumerable<OpenZWaveDeviceValue> values, IOpenZWaveDeviceValueMatcher matcher)
        {
            var results = values.Where(matcher.Matches);

            return results;
        }

        public static OpenZWaveDeviceValue Match(this IEnumerable<OpenZWaveDeviceValue> values, IOpenZWaveDeviceValueMatcher matcher)
        {
            var result = values.Matches(matcher).FirstOrDefault();

            return result;
        }

        public static OpenZWaveDeviceValue Match(this IEnumerable<OpenZWaveDeviceValue> values, byte id, CommandClass commandClass, byte index, byte? instance = null)
        {
            var matcher = CompositeMatcher.Create(id, commandClass, index, instance);
            var result = values.Match(matcher);

            return result;
        }
    }
}
