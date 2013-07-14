using System.Collections.Generic;
using System.Linq;

namespace Roomie.Common.HomeAutomation.Thermostats.Fans
{
    public class ReadOnlyThermostatFanState : IThermostatFanState
    {
        public IEnumerable<ThermostatFanMode> SupportedModes { get; private set; }
        public ThermostatFanMode? Mode { get; private set; }
        public ThermostatFanCurrentAction? CurrentAction { get; private set; }

        public static ReadOnlyThermostatFanState CopyFrom(IThermostatFanState state)
        {
            var result = new ReadOnlyThermostatFanState
            {
                SupportedModes = state.SupportedModes.ToList(),
                Mode = state.Mode,
                CurrentAction = state.CurrentAction
            };

            return result;
        }
    }
}
