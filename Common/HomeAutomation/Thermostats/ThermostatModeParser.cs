
namespace Roomie.Common.HomeAutomation.Thermostats
{
    public static class ThermostatModeParser
    {
        public static bool IsValid(string input)
        {
            return EnumParser.IsValid<ThermostatMode>(input);
        }

        public static ThermostatMode Parse(string input)
        {
            return EnumParser.Parse<ThermostatMode>(input);
        }

        public static ThermostatMode ToThermostatMode(this string input)
        {
            return Parse(input);
        }
    }
}
