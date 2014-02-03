﻿using Roomie.Common.HomeAutomation.DimmerSwitches;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.ToggleSwitches;

namespace Roomie.Common.HomeAutomation
{
    public interface IDevice : IDeviceState, IDeviceActions
    {
        INetwork Network { get; }
        IBinarySwitch ToggleSwitch { get; }
        IDimmerSwitch DimmerSwitch { get; }
        IThermostat Thermostat { get; }
        IKeypad Keypad { get; }
    }
}
