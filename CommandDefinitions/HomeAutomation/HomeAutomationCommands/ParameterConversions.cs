using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public static class ParameterConversions
    {
        public static IEventType ToEventType(this IParameter parameter)
        {
            return EventTypeParser.Parse(parameter.Value);
        }

        public static ThermostatSetpointType ToThermostatSetpointType(this IParameter parameter)
        {
            return ThermostatSetpointTypeParser.Parse(parameter.Value);
        }

        public static ThermostatFanMode ToThermostatFanMode(this IParameter parameter)
        {
            return ThermostatFanModeParser.Parse(parameter.Value);
        }

        public static ThermostatMode ToThermostatMode(this IParameter parameter)
        {
            return ThermostatModeParser.Parse(parameter.Value);
        }

        public static VirtualAddress ToVirtualAddress(this IParameter parameter)
        {
            return VirtualAddress.Parse(parameter.Value);
        }
    }
}
