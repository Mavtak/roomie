using Roomie.Common;
using Roomie.Common.Color;
using Roomie.Common.Measurements.Temperature;

namespace Roomie.Desktop.Engine.Parameters
{
    public static class ParameterValidations
    {
        public static bool IsBoolean(this IParameter parameter)
        {
            bool value;
            var result = bool.TryParse(parameter.Value, out value);

            return result;
        }

        public static bool IsByte(this IParameter parameter)
        {
            byte value;
            var result = byte.TryParse(parameter.Value, out value);

            return result;
        }

        public static bool IsColor(this IParameter parameter)
        {
            return ColorParser.IsValid(parameter.Value);
        }

        public static bool IsDateTime(this IParameter parameter)
        {
            return TimeUtils.IsDateTime(parameter.Value);
        }

        public static bool IsInteger(this IParameter parameter)
        {
            int value;
            var result = int.TryParse(parameter.Value, out value);

            return result;
        }

        public static bool IsInteger(this IParameter parameter, int? min, int? max)
        {
            int value;
            var result = int.TryParse(parameter.Value, out value);

            if (!result)
            {
                return false;
            }

            if (value < min)
            {
                return false;
            }

            if (value > max)
            {
                return false;
            }

            return true;
        }

        public static bool IsTemperature(this IParameter parameter)
        {
            return TemperatureParser.IsTemperature(parameter.Value);
        }

        public static bool IsTimeSpan(this IParameter parameter)
        {
            return TimeUtils.IsTimeSpan(parameter.Value);
        }
    }
}
