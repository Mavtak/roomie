﻿using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.MultilevelSwitches;
using Roomie.Common.HomeAutomation.Thermostats;

namespace Roomie.Common.HomeAutomation
{
    public interface IDeviceActions
    {
        IBinarySwitchActions BinarySwitchActions { get; }
        IMultilevelSwitchActions DimmerSwitchActions { get; }
        IThermostatActions ThermostatActions { get; }
        IKeypadActions KeypadActions { get; }

        void Poll();
    }
}
