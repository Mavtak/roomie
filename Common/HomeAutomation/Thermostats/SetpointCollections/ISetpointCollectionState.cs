using System.Collections.Generic;
using Roomie.Common.Temperature;

namespace Roomie.Common.HomeAutomation.Thermostats.SetpointCollections
{
    public interface IThermostatSetpointCollectionState
    {
        ITemperature this[ThermostatSetpointType setpoint] { get; }
        IEnumerable<ThermostatSetpointType> AvailableSetpoints { get; }
    }
}
