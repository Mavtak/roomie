using System;
using System.Linq;
using System.Text;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.OpenZWaveDeviceValueMatchers
{
    public class LabelMatcher : IOpenZWaveDeviceValueMatcher
    {
        private readonly string[] _label;

        public LabelMatcher(params string[] units)
        {
            _label = units;
        }

        public bool Matches(OpenZWaveDeviceValue entry)
        {
            var result = _label.Any(x => string.Equals(x, entry.Label, StringComparison.InvariantCultureIgnoreCase));

            return result;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append("Label ");

            if (_label.Length == 1)
            {
                result.Append("= ");
                result.Append(_label.First());
            }
            else
            {
                result.Append("in (");
                result.Append(string.Join(",", _label));
                result.Append(")");
            }

            return result.ToString();
        }

        public static IOpenZWaveDeviceValueMatcher Temperature()
        {
            var result = new UnitsMatcher("Temperature");

            return result;
        }

        public static IOpenZWaveDeviceValueMatcher Luminance()
        {
            var result = new UnitsMatcher("Luminance");

            return result;
        }

        public static IOpenZWaveDeviceValueMatcher Humidity()
        {
            var result = new UnitsMatcher("Relative Humidity");

            return result;
        }
    }
}
