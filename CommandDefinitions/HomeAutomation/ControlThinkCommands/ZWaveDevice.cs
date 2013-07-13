﻿using System;
using Roomie.CommandDefinitions.HomeAutomationCommands.Exceptions;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.DimmerSwitches;
using Roomie.Common.HomeAutomation.Exceptions;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.ToggleSwitches;
using BaseDevice = Roomie.CommandDefinitions.HomeAutomationCommands.Device;
using BaseNetwork = Roomie.CommandDefinitions.HomeAutomationCommands.Network;
using BackingDevice = ControlThink.ZWave.Devices.ZWaveDevice;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    public class ZWaveDevice : BaseDevice
    {
        internal BackingDevice BackingObject { get; private set; }
        private readonly ZWaveToggleSwitch _toggleSwitch;
        private readonly ZWaveDimmerSwitch _dimmerSwitch;
        private readonly ZWaveThermostat _thermostat;

        //TODO: move this into ZWaveDimmerSwitch
        internal byte? CachedPower
        {
            get
            {
                return (byte?)power;
            }
            set
            {
                power = value;
            }
        }

        public ZWaveDevice(BaseNetwork network, BackingDevice backingDevice, DeviceType type = null, string name = null)
            : base(network, 99, type, name)
        {
            BackingObject = backingDevice;

            BackingObject.PollEnabled = false;

            Address = backingDevice.NodeID.ToString();

            if (type != null && type.CanControl && !type.CanDim)
            {
                MaxPower = 255;
            }

            BackingObject.LevelChanged += (sender, args) =>
                {
                    CachedPower = args.Level;
                    IsConnected = true;
                    PowerChanged();
                };

            _toggleSwitch = new ZWaveToggleSwitch(this);
            _dimmerSwitch = new ZWaveDimmerSwitch(this);
            _thermostat = new ZWaveThermostat(this);

        }

        public override IToggleSwitch ToggleSwitch
        {
            get
            {
                return _toggleSwitch;
            }
        }

        public override IDimmerSwitch DimmerSwitch
        {
            get
            {
                return _dimmerSwitch;
            }
        }

        public override IThermostat Thermostat
        {
            get
            {
                return _thermostat;
            }
        }
    }
}
