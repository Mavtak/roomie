
using Roomie.Common.HomeAutomation.Thermostats.Fans;
namespace Roomie.Common.HomeAutomation.Thermostats
{
    public interface IThermostat : IThermostatState, IThermostatActions
    {
        IThermostatFan Fan { get; }
        ISetpointCollection Setpoints { get; }
    }
}
