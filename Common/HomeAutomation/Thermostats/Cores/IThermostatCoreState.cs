using System.Collections.Generic;

namespace Roomie.Common.HomeAutomation.Thermostats.Cores
{
    public interface IThermostatCoreState
    {
        IEnumerable<ThermostatMode> SupportedModes { get; }
        ThermostatMode? Mode { get; }
        ThermostatCurrentAction? CurrentAction { get; }
    }
}
