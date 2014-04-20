using Roomie.Common.HomeAutomation.BinarySensors;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.MultilevelSensors;
using Roomie.Common.HomeAutomation.MultilevelSwitches;
using Roomie.Common.HomeAutomation.Thermostats;

namespace Roomie.Common.HomeAutomation
{
    public interface IDeviceActions
    {
        IBinarySwitchActions BinarySwitchActions { get; }
        IMultilevelSensorActions PowerSensorActions { get; }
        IMultilevelSensorActions HumiditySensorActions { get; }
        IMultilevelSwitchActions MultilevelSwitchActions { get; }
        IBinarySensorActions BinarySensorActions { get; }
        IThermostatActions ThermostatActions { get; }
        IKeypadActions KeypadActions { get; }

        void Poll();
    }
}
