
namespace Roomie.Common.HomeAutomation.Thermostats
{
    public interface IThermostatActions
    {
        void PollTemperature();
        void PollCurrentAction();
        void SetMode(ThermostatMode mode);
    }
}
