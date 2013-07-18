using System.Collections.Generic;
using System.Linq;

namespace Roomie.Common.HomeAutomation.Thermostats.Fans
{
    public class ReadOnlyThermostatFanState : IThermostatFanState
    {
        public IEnumerable<ThermostatFanMode> SupportedModes { get; private set; }
        public ThermostatFanMode? Mode { get; private set; }
        public ThermostatFanCurrentAction? CurrentAction { get; private set; }

        public ReadOnlyThermostatFanState()
        {
        }

        public ReadOnlyThermostatFanState(IEnumerable<ThermostatFanMode> supportedModes, ThermostatFanMode? mode, ThermostatFanCurrentAction? currentAction)
        {
            SupportedModes = supportedModes;
            Mode = mode;
            CurrentAction = currentAction;
        }

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

        public static ReadOnlyThermostatFanState Empty()
        {
            var result = new ReadOnlyThermostatFanState
                {
                    SupportedModes = new ThermostatFanMode[] {}
                };

            return result;
        }
    }
}
