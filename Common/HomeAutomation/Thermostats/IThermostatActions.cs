
namespace Roomie.Common.HomeAutomation.Thermostats
{
    public interface IThermostatActions
    {
        void PollTemperature();
        void PollCurrentAction();
        void PollMode();
        void PollSupportedModes();
        void SetMode(ThermostatMode mode);
    }
}
