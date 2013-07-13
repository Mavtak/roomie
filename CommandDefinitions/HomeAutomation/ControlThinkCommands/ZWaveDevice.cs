using System;
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

        public ZWaveDevice(BaseNetwork network, BackingDevice backingDevice, DeviceType type = null, string name = null)
            : base(network, type, name)
        {
            BackingObject = backingDevice;

            BackingObject.PollEnabled = false;

            Address = backingDevice.NodeID.ToString();

            _toggleSwitch = new ZWaveToggleSwitch(this);
            _dimmerSwitch = new ZWaveDimmerSwitch(this);
            _thermostat = new ZWaveThermostat(this);

            if (Type.CanDim)
            {
                _dimmerSwitch.MaxPower = 99;
            }
            else
            {
                _dimmerSwitch.MaxPower = 255;
            }

            BackingObject.LevelChanged += (sender, args) =>
                {
                    _dimmerSwitch.Power = args.Level;
                    IsConnected = true;
                    //TODO: device found event?
                    PowerChanged();
                };
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
