
namespace Roomie.Common.HomeAutomation.Thermostats.Fans
{
    public interface IThermostatFanActions
    {
        void PollCurrentAction();
        void PollMode();
        void PollSupportedModes();
        void SetMode(ThermostatFanMode fanMode);
    }
}
