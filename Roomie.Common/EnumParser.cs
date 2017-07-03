using System;
using System.Linq;

namespace Roomie.Common
{
    public static class EnumParser
    {
        public static bool IsValid<TEnum>(string input)
            where TEnum : struct
        {
            var result = TryParse<TEnum>(input) != null;

            return result;
        }

        public static TEnum Parse<TEnum>(string input)
            where TEnum : struct
        {
            var result = TryParse<TEnum>(input).Value;

            return result;
        }

        public static TEnum ToSetpointType<TEnum>(this string input)
            where TEnum : struct
        {
            return Parse<TEnum>(input);
        }

        private static TEnum? TryParse<TEnum>(string input)
            where TEnum : struct
        {
            var choices = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

            foreach (var choice in choices)
            {
                if (String.Equals(choice.ToString(), input, StringComparison.InvariantCultureIgnoreCase))
                {
                    return choice;
                }
            }

            return null;
        }
    }
}
