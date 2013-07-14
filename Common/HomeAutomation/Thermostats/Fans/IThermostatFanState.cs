using System.Collections.Generic;

namespace Roomie.Common.HomeAutomation.Thermostats.Fans
{
    public interface IThermostatFanState
    {
        IEnumerable<ThermostatFanMode> SupportedModes { get; }
        ThermostatFanMode? Mode { get; }
        ThermostatFanCurrentAction? CurrentAction { get; }
    }
}
