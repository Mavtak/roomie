using System;
using System.Linq;

namespace Roomie.Common.HomeAutomation.Thermostats
{
    public static class SetpointTypeParser
    {
        public static bool IsValid(string input)
        {
            var result = TryParse(input) != null;

            return result;
        }

        public static SetpointType Parse(string input)
        {
            var result = TryParse(input).Value;

            return result;
        }

        private static SetpointType? TryParse(string input)
        {
            var setPoints = Enum.GetValues(typeof(SetpointType)).Cast<SetpointType>();

            foreach (var setpoint in setPoints)
            {
                if (String.Equals(setpoint.ToString(), input, StringComparison.InvariantCultureIgnoreCase))
                {
                    return setpoint;
                }
            }

            return null;
        }
    }
}
