using Roomie.Common.Temperature;

namespace Roomie.Common.HomeAutomation.Thermostats
{
    public interface ISetpointCollectionActions
    {
        void SetSetpoint(SetpointType setpointType, ITemperature temperature);
    }
}
