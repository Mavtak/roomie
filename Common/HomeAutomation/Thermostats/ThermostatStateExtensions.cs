using System.Text;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;

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

            if (state.CurrentAction != null)
            {
                if (result.Length > 0)
                {
                    result.Append(", ");
                }

                result.Append("action: ");
                result.Append(state.CurrentAction);
            }

            if (state.FanState.Mode != null)
            {
                if (result.Length > 0)
                {
                    result.Append(", ");
                }

                result.Append("Fan mode: ");
                result.Append(state.FanState.Mode);
            }

            if (state.FanState.CurrentAction != null)
            {
                if (result.Length > 0)
                {
                    result.Append(", ");
                }

                result.Append("Fan action: ");
                result.Append(state.FanState.CurrentAction);
            }

            var setpointsDescription = state.SetPointStates.Describe();
            if (!string.IsNullOrEmpty(setpointsDescription))
            {
                if (result.Length > 0)
                {
                    result.Append(", ");
                }

                result.Append("Setpoints: ");
                result.Append(setpointsDescription);
            }

            return result.ToString();
        }
    }
}
