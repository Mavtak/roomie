using System;
using Roomie.Common;
using Roomie.Common.Color;
using Roomie.Common.Measurements.Temperature;

namespace Roomie.Desktop.Engine.Parameters
{
    public static class ParameterValidations
    {
        public static bool IsBoolean(this IParameter parameter)
        {
            try
            {
                Convert.ToBoolean(parameter.Value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsByte(this IParameter parameter)
        {
            try
            {
                Convert.ToByte(parameter.Value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
            try
            {
                Convert.ToInt64(parameter.Value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsInteger(this IParameter parameter, int? min, int? max)
        {
            try
            {
                var number = Convert.ToInt64(parameter.Value);

                if (number < min)
                {
                    return false;
                }

                if (number > max)
                {
                    return false;
                }
            }
            catch (Exception)
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
