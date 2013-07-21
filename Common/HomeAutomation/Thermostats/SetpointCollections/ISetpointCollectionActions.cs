using Roomie.Common.Temperature;

namespace Roomie.Common.HomeAutomation.Thermostats.SetpointCollections
{
    public interface ISetpointCollectionActions
    {
        void PollSupportedSetpoints();
        void PollSetpointTemperatures();
        void SetSetpoint(SetpointType setpointType, ITemperature temperature);
    }
}
