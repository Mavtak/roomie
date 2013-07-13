using Roomie.Common.HomeAutomation.DimmerSwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.ToggleSwitches;

namespace Roomie.Common.HomeAutomation
{
    public interface IDeviceState
    {
        IToggleSwitchState ToggleSwitchState { get; }
        IDimmerSwitchState DimmerSwitchState { get; }
        IThermostatState ThermostatState { get; }   
    }
}
