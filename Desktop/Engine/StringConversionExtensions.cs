using System;

namespace Roomie.Desktop.Engine
{
    public static class StringConversionExtensions
    {
        public static bool ToBoolean(this string input)
        {
            return Convert.ToBoolean(input);
        }

        public static int ToInteger(this string input)
        {
            return Convert.ToInt32(input);
        }

        public static byte ToByte(this string input)
        {
            return Convert.ToByte(input);
        }
    }
}
