using System;

namespace Roomie.Desktop.Engine
{
    public static class StringConversionExtensions
    {
        public static bool ToBoolean(this string input)
        {
            return Convert.ToBoolean(input);
        }
    }
}
