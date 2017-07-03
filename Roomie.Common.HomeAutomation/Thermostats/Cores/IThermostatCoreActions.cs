namespace Roomie.Common.HomeAutomation.Thermostats.Cores
{
    public interface IThermostatCoreActions
    {
        void PollCurrentAction();
        void PollMode();
        void PollSupportedModes();
        void SetMode(ThermostatMode mode);
    }
}
