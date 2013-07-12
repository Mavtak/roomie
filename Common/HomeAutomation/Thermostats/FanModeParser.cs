
namespace Roomie.Common.HomeAutomation.Thermostats
{
    public static class FanModeParser
    {
        public static bool IsValid(string input)
        {
            return EnumParser.IsValid<FanMode>(input);
        }

        public static FanMode Parse(string input)
        {
            return EnumParser.Parse<FanMode>(input);
        }

        public static FanMode ToFanMode(this string input)
        {
            return Parse(input);
        }
    }
}
