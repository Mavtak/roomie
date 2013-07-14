
namespace Roomie.Common.HomeAutomation.Thermostats.Fans
{
    public static class ThermostatFanModeParser
    {
        public static bool IsValid(string input)
        {
            return EnumParser.IsValid<ThermostatFanMode>(input);
        }

        public static ThermostatFanMode Parse(string input)
        {
            return EnumParser.Parse<ThermostatFanMode>(input);
        }

        public static ThermostatFanMode ToFanMode(this string input)
        {
            return Parse(input);
        }
    }
}
