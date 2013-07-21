
namespace Roomie.Common.HomeAutomation.Thermostats.Fans
{
    public interface IThermostatFanActions
    {
        void PollCurrentAction();
        void PollMode();
        void SetMode(ThermostatFanMode fanMode);
    }
}
