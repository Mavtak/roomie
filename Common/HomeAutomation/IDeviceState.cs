﻿using Roomie.Common.HomeAutomation.DimmerSwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.ToggleSwitches;

namespace Roomie.Common.HomeAutomation
{
    public interface IDeviceState
    {
        string Name { get; }
        //TODO: change to NetworkAddress
        string Address { get; }
        DeviceLocation Location { get; }
        //TODO: convert to interface
        Network Network { get; }
        bool? IsConnected { get; }
        DeviceType Type { get; }
        IToggleSwitchState ToggleSwitchState { get; }
        IDimmerSwitchState DimmerSwitchState { get; }
        IThermostatState ThermostatState { get; }   
    }
}