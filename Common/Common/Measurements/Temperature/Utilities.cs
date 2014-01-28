using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Roomie.Common.Tests")]

namespace Roomie.Common.Temperature
{
    internal static class Utilities
    {
        //TODO unit test this
        internal static bool EqualsHelper(ITemperature one, ITemperature two)
        {
            if (one == two)
            {
                return true;
            }

            if (one == null || two == null)
            {
                return false;
            }

            var result = Math.Abs(one.Celsius.Value - two.Celsius.Value) < .001;

            return result;
        }

        internal static bool EqualsHelper(ITemperature one, object obj)
        {
            var two = obj as ITemperature;

            if (two == null && obj != null)
            {
                return false;
            }

            return EqualsHelper(one, two);
        }
    }
}
