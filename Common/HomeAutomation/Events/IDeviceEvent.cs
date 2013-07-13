using Roomie.Common.HomeAutomation.Thermostats;

namespace Roomie.Common.HomeAutomation.Events
{
    public interface IDeviceEvent : IEvent
    {
        Device Device { get; }
        int? Power { get; }
        IThermostatState ThermostatState { get; }
    }
}
