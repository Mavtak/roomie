using Roomie.Common.HomeAutomation.DimmerSwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.ToggleSwitches;

namespace Roomie.Common.HomeAutomation
{
    public interface IDeviceActions
    {
        IToggleSwitchActions ToggleSwitchActions { get; }
        IDimmerSwitchActions DimmerSwitchActions { get; }
        IThermostatActions ThermostatActions { get; }

        void Poll();
    }
}
