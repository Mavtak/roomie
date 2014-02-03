using Roomie.Common.HomeAutomation.BinarySensors;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.MultilevelSwitches;
using Roomie.Common.HomeAutomation.Thermostats;

namespace Roomie.Common.HomeAutomation
{
    public interface IDevice : IDeviceState, IDeviceActions
    {
        INetwork Network { get; }
        IBinarySwitch BinarySwitch { get; }
        IMultilevelSwitch MultilevelSwitch { get; }
        IBinarySensor BinarySensor { get;}
        IThermostat Thermostat { get; }
        IKeypad Keypad { get; }
    }
}
