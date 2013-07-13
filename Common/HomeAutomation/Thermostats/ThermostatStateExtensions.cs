using System.Text;

namespace Roomie.Common.HomeAutomation.Thermostats
{
    public static class ThermostatStateExtensions
    {
        public static ReadOnlyThermostatState Copy(this IThermostatState state)
        {
            return ReadOnlyThermostatState.CopyFrom(state);
        }

        public static string Describe(this IThermostatState state)
        {
            var result = new StringBuilder();

            if (state == null)
            {
                return result.ToString();
            }

            if (state.Temperature != null)
            {
                result.Append(state.Temperature);
            }

            return result.ToString();
        }
    }
}
