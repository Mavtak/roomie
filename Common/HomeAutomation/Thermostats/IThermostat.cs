using Roomie.Common.Temperature;

namespace Roomie.Common.HomeAutomation.Thermostats
{
    public interface IThermostat : IThermostatState
    {
        void PollTemperature();
        void SetSetpoint(SetpointType setpointType, ITemperature temperature);
        void SetFanMode(FanMode fanMode);
    }
}
