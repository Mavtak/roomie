using Roomie.Common.HomeAutomation.Thermostats.Cores;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;

namespace Roomie.Common.HomeAutomation.Thermostats
{
    public interface IThermostat : IThermostatState, IThermostatActions
    {
        IThermostatCore Core { get; }
        IThermostatFan Fan { get; }
        ISetpointCollection Setpoints { get; }
    }
}
