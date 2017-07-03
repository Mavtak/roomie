using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public static class ParameterValidations
    {
        public static bool IsEventType(this IParameter parameter)
        {
            return EventTypeParser.IsValid(parameter.Value);
        }

        public static bool IsThermostatSetpointType(this IParameter parameter)
        {
            return ThermostatSetpointTypeParser.IsValid(parameter.Value);
        }

        public static bool IsThermostatFanMode(this IParameter parameter)
        {
            return ThermostatFanModeParser.IsValid(parameter.Value);
        }

        public static bool IsThermostatMode(this IParameter parameter)
        {
            return ThermostatModeParser.IsValid(parameter.Value);
        }

        public static bool IsVirtualAddress(this IParameter parameter)
        {
            return VirtualAddress.IsValid(parameter.Value);
        }
    }
}
