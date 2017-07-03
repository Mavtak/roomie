using System;
using Roomie.Common.Measurements;

namespace Roomie.Common.HomeAutomation.MultilevelSensors
{
    public interface IMultilevelSensorState<TMeasurement>
        where TMeasurement : IMeasurement
    {
        TMeasurement Value { get; }
        DateTime? TimeStamp { get; }
    }
}
