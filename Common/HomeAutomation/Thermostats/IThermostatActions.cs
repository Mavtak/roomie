using Roomie.Common.Temperature;

namespace Roomie.Common.HomeAutomation.Thermostats
{
    public interface IThermostatActions
    {
        void PollTemperature();
        void SetSetpoint(SetpointType setpointType, ITemperature temperature);
        void SetFanMode(FanMode fanMode);
    }
}
