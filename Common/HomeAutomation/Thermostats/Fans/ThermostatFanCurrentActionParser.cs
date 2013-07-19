
namespace Roomie.Common.HomeAutomation.Thermostats.Fans
{
    public static class ThermostatFanCurrentActionParser
    {
        public static bool IsValid(string input)
        {
            return EnumParser.IsValid<ThermostatFanCurrentAction>(input);
        }

        public static ThermostatFanCurrentAction Parse(string input)
        {
            return EnumParser.Parse<ThermostatFanCurrentAction>(input);
        }

        public static ThermostatFanCurrentAction ToThermostatFanCurrentAction(this string input)
        {
            return Parse(input);
        }
    }
}
