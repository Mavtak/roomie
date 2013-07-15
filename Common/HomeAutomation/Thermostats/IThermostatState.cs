using System.Collections.Generic;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.Temperature;

namespace Roomie.Common.HomeAutomation.Thermostats
{
    public interface IThermostatState
    {
        ITemperature Temperature { get; }

        IThermostatFanState FanState { get; }

        IEnumerable<ThermostatMode> SupportedModes { get; }
        ThermostatMode? Mode { get; }
        ThermostatCurrentAction? CurrentAction { get; }

        ISetpointCollectionState SetPointStates { get; }
        
    }
}
