
namespace Roomie.Common.HomeAutomation.Thermostats
{
    public interface IThermostatActions
    {
        void PollTemperature();
        void SetMode(ThermostatMode mode);
    }
}
