using System;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.BinarySensors;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.MultilevelSwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using BackingDevice = ControlThink.ZWave.Devices.ZWaveDevice;
using BaseDevice = Roomie.CommandDefinitions.HomeAutomationCommands.Device;
using BaseNetwork = Roomie.CommandDefinitions.HomeAutomationCommands.Network;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    internal class ZWaveDevice : BaseDevice
    {
        internal BackingDevice BackingObject { get; private set; }
        private readonly ZWaveToggleSwitch _toggleSwitch;
        private readonly ZWaveDimmerSwitch _dimmerSwitch;
        private readonly ZWaveThermostat _thermostat;
        private readonly IBinarySensor _binarySensor; //TODO: implement

        public ZWaveDevice(BaseNetwork network, BackingDevice backingDevice, DeviceType type = null, string name = null)
            : base(network, type, name)
        {
            BackingObject = backingDevice;

            BackingObject.PollEnabled = false;

            Address = backingDevice.NodeID.ToString();

            _toggleSwitch = new ZWaveToggleSwitch(this);
            _dimmerSwitch = new ZWaveDimmerSwitch(this);
            _thermostat = new ZWaveThermostat(this);
            _binarySensor = null; //TODO: implement

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

        protected void PowerChanged()
        {
            //TODO: improve this logic
            //TODO: read from event history in making powered on/off decision
            IDeviceEvent @event = null;
            IEventSource source = null; //TODO: fill this in
            if (IsConnected == true)
            {
                if (Type.Equals(DeviceType.BinarySensor))
                {
                    switch (BinarySwitch.Power)
                    {
                        case BinarySwitchPower.On:
                            @event = DeviceEvent.MotionDetected(this, source);
                            break;

                        case BinarySwitchPower.Off:
                            @event = DeviceEvent.StillnessDetected(this, source);
                            break;
                    }
                }
                else
                {
                    if (Type.Equals(DeviceType.BinarySwitch))
                    {
                        switch (BinarySwitch.Power)
                        {
                            case BinarySwitchPower.On:
                                @event = DeviceEvent.PoweredOn(this, source);
                                break;

                            case BinarySwitchPower.Off:
                                @event = DeviceEvent.PoweredOff(this, source);
                                break;
                        }
                    }
                }
            }
            else
            {
                @event = DeviceEvent.Lost(this, source);
            }

            if (@event == null)
            {
                @event = DeviceEvent.PowerChanged(this, source);
            }

            AddEvent(@event);
        }

        public override IBinarySwitch BinarySwitch
        {
            get
            {
                return _toggleSwitch;
            }
        }

        public override IMultilevelSwitch MultilevelSwitch
        {
            get
            {
                return _dimmerSwitch;
            }
        }

        public override IBinarySensor BinarySensor
        {
            get
            {
                return _binarySensor;
            }
        }

        public override IThermostat Thermostat
        {
            get
            {
                return _thermostat;
            }
        }

        public override IKeypad Keypad
        {
            get
            {
                return null;
            }
        }
    }
}
