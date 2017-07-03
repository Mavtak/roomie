using Roomie.Common.Measurements;

namespace Roomie.Common.HomeAutomation.MultilevelSensors
{
    public interface IMultilevelSensor<TMeasurement> : IMultilevelSensorState<TMeasurement>, IMultilevelSensorActions
        where TMeasurement : IMeasurement
    {
    }
}
