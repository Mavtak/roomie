using Roomie.Common.Temperature;

namespace Roomie.Common.HomeAutomation.Thermostats.SetpointCollections
{
    public interface IThermostatSetpointCollectionActions
    {
        void PollSupportedSetpoints();
        void PollSetpointTemperatures();
        void SetSetpoint(ThermostatSetpointType setpointType, ITemperature temperature);
    }
}
