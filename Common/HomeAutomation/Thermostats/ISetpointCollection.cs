using System.Collections.Generic;
using Roomie.Common.Temperature;

namespace Roomie.Common.HomeAutomation.Thermostats
{
    public interface ISetpointCollection
    {
        ITemperature this[SetpointType setpoint] { get; }
        IEnumerable<SetpointType> AvailableSetpoints { get; }
    }
}
