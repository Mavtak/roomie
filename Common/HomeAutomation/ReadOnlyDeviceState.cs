using Roomie.Common.HomeAutomation.DimmerSwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.ToggleSwitches;

namespace Roomie.Common.HomeAutomation
{
    public class ReadOnlyDeviceState : IDeviceState
    {
        public IToggleSwitchState ToggleSwitchState { get; private set; }
        public IDimmerSwitchState DimmerSwitchState { get; private set; }
        public IThermostatState ThermostatState { get; private set; }

        public static IDeviceState CopyFrom(IDeviceState source)
        {
            var result = new ReadOnlyDeviceState
            {
                ToggleSwitchState = source.ToggleSwitchState.Copy(),
                DimmerSwitchState = source.DimmerSwitchState.Copy(),
                ThermostatState = source.ThermostatState.Copy()
            };

            return result;
        }
    }
}
