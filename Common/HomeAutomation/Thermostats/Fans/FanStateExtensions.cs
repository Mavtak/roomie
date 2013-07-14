
namespace Roomie.Common.HomeAutomation.Thermostats.Fans
{
    public static class ThermostatFanStateExtensions
    {
        public static ReadOnlyThermostatFanState Copy(this IThermostatFanState state)
        {
            return ReadOnlyThermostatFanState.CopyFrom(state);
        }
    }
}
