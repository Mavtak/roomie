using Roomie.Common.HomeAutomation.DimmerSwitches;
using Roomie.Common.HomeAutomation.Thermostats;

namespace Roomie.Common.HomeAutomation.Events
{
    public interface IDeviceEvent : IEvent
    {
        Device Device { get; }
        IDimmerSwitchState DimmerSwitchState { get; }
        IThermostatState ThermostatState { get; }
    }
}
