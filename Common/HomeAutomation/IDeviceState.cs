﻿using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.DimmerSwitches;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.Thermostats;

namespace Roomie.Common.HomeAutomation
{
    public interface IDeviceState : IHasName
    {
        //TODO: change to NetworkAddress
        string Address { get; }
        ILocation Location { get; }
        //TODO: convert to interface
        INetworkState NetworkState { get; }
        bool? IsConnected { get; }
        DeviceType Type { get; }
        IBinarySwitchState ToggleSwitchState { get; }
        IDimmerSwitchState DimmerSwitchState { get; }
        IThermostatState ThermostatState { get; }
        IKeypadState KeypadState { get; }

        void Update(IDeviceState state);
    }
}
