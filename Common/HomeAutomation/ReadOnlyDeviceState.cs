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
                ToggleSwitchState = ReadOnlyToggleSwitchState.CopyTo(source.ToggleSwitchState),
                DimmerSwitchState = ReadOnlyDimmerSwitchState.CopyFrom(source.DimmerSwitchState),
                ThermostatState = ReadOnlyThermostatState.CopyFrom(source.ThermostatState)
            };

            return result;
        }
    }
}
