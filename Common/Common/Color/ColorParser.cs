using System;

namespace Roomie.Common.Color
{
    public static class ColorParser
    {
        //TODO: add tests
        public static bool IsValid(string input)
        {
            var result = TryParse(input) != null;

            return result;
        }

        public static IColor Parse(string input)
        {
            var result = TryParse(input);

            if (input == null)
            {
                throw new Exception("Invalid color \"" + input + "\"");
            }

            return result;
        }

        public static IColor TryParse(string input)
        {
            var result = ColorExtensions.FromHexString(input);
            
            if (result != null)
            {
                return result;
            }

            result = Utilities.NameDictionary.Find(input);

            return result;
        }

        public static IColor ToColor(this string input)
        {
            return Parse(input);
        }
    }
}
