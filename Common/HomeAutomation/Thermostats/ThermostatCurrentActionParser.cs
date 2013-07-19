
namespace Roomie.Common.HomeAutomation.Thermostats
{
    public static class ThermostatCurrentActionParser
    {
        public static bool IsValid(string input)
        {
            return EnumParser.IsValid<ThermostatCurrentAction>(input);
        }

        public static ThermostatCurrentAction Parse(string input)
        {
            return EnumParser.Parse<ThermostatCurrentAction>(input);
        }

        public static ThermostatCurrentAction ToThermostatCurrentAction(this string input)
        {
            return Parse(input);
        }
    }
}
