using Roomie.Common.HomeAutomation.DimmerSwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.ToggleSwitches;

namespace Roomie.Common.HomeAutomation.Events
{
    public interface IDeviceEvent : IEvent
    {
        Device Device { get; }
        IToggleSwitchState ToggleSwitchState { get; }
        IDimmerSwitchState DimmerSwitchState { get; }
        IThermostatState ThermostatState { get; }
    }
}
