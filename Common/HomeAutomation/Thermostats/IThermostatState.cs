using System.Collections.Generic;
using Roomie.Common.HomeAutomation.Thermostats.Cores;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;

namespace Roomie.Common.HomeAutomation.Thermostats
{
    public interface IThermostatState
    {
        IThermostatCoreState CoreState { get; }
        IThermostatFanState FanState { get; }
        IThermostatSetpointCollectionState SetpointStates { get; }
        
    }
}
