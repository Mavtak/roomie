using System;
using Roomie.Common;
using Roomie.Common.Color;
using Roomie.Common.Measurements.Temperature;

namespace Roomie.Desktop.Engine.Parameters
{
    public static class ParameterConversions
    {
        public static bool ToBoolean(this IParameter parameter)
        {
            return bool.Parse(parameter.Value);
        }

        public static byte ToByte(this IParameter parameter)
        {
            return byte.Parse(parameter.Value);
        }

        public static IColor ToColor(this IParameter parameter)
        {
            return ColorParser.Parse(parameter.Value);
        }

        public static DateTime ToDateTime(this IParameter parameter)
        {
            return TimeUtils.ToDateTime(parameter.Value);
        }

        public static int ToInteger(this IParameter parameter)
        {
            return int.Parse(parameter.Value);
        }

        public static ITemperature ToTemperature(this IParameter parameter)
        {
            return TemperatureParser.Parse(parameter.Value);
        }

        public static TimeSpan ToTimeSpan(this IParameter parameter)
        {
            return TimeUtils.ToTimeSpan(parameter.Value);
        }
    }
}
