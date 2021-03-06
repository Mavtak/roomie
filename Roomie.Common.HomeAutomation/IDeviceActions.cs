﻿using Roomie.Common.HomeAutomation.BinarySensors;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.ColorSwitch;
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
        IMultilevelSensorActions TemperatureSensorActions { get; }
        IMultilevelSensorActions HumiditySensorActions { get; }
        IMultilevelSensorActions IlluminanceSensorActions { get; }
        IMultilevelSwitchActions MultilevelSwitchActions { get; }
        IColorSwitchActions ColorSwitchActions { get; }
        IBinarySensorActions BinarySensorActions { get; }
        IThermostatActions ThermostatActions { get; }
        IKeypadActions KeypadActions { get; }

        void Poll();
    }
}
