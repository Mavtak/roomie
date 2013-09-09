using System.Collections.Generic;
using Roomie.Common.HomeAutomation.Thermostats.Cores;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Temperature;

namespace Roomie.Common.HomeAutomation.Thermostats
{
    public interface IThermostatState
    {
        ITemperature Temperature { get; }

        IThermostatCoreState CoreState { get; }
        IThermostatFanState FanState { get; }
        IThermostatSetpointCollectionState SetpointStates { get; }
        
    }
}
