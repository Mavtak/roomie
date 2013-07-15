
namespace Roomie.Common.HomeAutomation.Thermostats
{
    public static class SetpointTypeParser
    {
        public static bool IsValid(string input)
        {
            return EnumParser.IsValid<SetpointType>(input);
        }

        public static SetpointType Parse(string input)
        {
            return EnumParser.Parse<SetpointType>(input);
        }

        public static SetpointType ToSetpointType(this string input)
        {
            return Parse(input);
        }
    }
}
