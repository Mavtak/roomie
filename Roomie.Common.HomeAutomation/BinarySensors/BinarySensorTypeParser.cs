
namespace Roomie.Common.HomeAutomation.BinarySensors
{
    public static class BinarySensorTypeParser
    {
        public static bool IsValid(string input)
        {
            return EnumParser.IsValid<BinarySensorType>(input);
        }

        public static BinarySensorType Parse(string input)
        {
            return EnumParser.Parse<BinarySensorType>(input);
        }

        public static BinarySensorType ToBinarySensorType(this string input)
        {
            return Parse(input);
        }
        public static BinarySensorType? ToBinarySensorTypeNullable(this string input)
        {
            if (input == null)
            {
                return null;
            }

            return Parse(input);
        }
    }
}
