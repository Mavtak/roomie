
namespace Roomie.Common.HomeAutomation.Thermostats.SetpointCollections
{
    public static class ThermostatSetpointTypeParser
    {
        public static bool IsValid(string input)
        {
            return EnumParser.IsValid<ThermostatSetpointType>(input);
        }

        public static ThermostatSetpointType Parse(string input)
        {
            return EnumParser.Parse<ThermostatSetpointType>(input);
        }

        public static ThermostatSetpointType ToSetpointType(this string input)
        {
            return Parse(input);
        }
    }
}
