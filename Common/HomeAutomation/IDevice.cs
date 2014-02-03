using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.DimmerSwitches;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.Thermostats;

namespace Roomie.Common.HomeAutomation
{
    public interface IDevice : IDeviceState, IDeviceActions
    {
        INetwork Network { get; }
        IBinarySwitch ToggleSwitch { get; }
        IMultilevelSwitch DimmerSwitch { get; }
        IThermostat Thermostat { get; }
        IKeypad Keypad { get; }
    }
}
