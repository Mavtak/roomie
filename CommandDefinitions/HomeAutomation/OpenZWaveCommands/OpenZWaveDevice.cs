﻿using System.Collections.Generic;
using OpenZWaveDotNet;
using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Common.HomeAutomation.BinarySensors;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.MultilevelSensors;
using Roomie.Common.HomeAutomation.MultilevelSwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.Measurements.Power;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWaveDevice : Device
    {
        internal List<OpenZWaveDeviceValue> Values { get; private set; }
        internal ZWManager Manager { get; private set; }
        internal byte Id { get; private set; }

        private readonly OpenZWaveToggleSwitch _toggleSwitch;
        private readonly OpenZWaveDimmerSwitch _dimmerSwitch;
        private readonly OpenZWaveThermostat _thermostat;
        private readonly OpenZWaveBinarySensor _binarySensor;
        private readonly OpenZWavePowerSensor _powerSensor;

        public byte? Event;

        public OpenZWaveDevice(Network network, ZWManager manager, byte id)
            : base(network)
        {
            Manager = manager;
            Id = id;
            Values = new List<OpenZWaveDeviceValue>();

            Address = Id.ToString();
            IsConnected = true;

            _toggleSwitch = new OpenZWaveToggleSwitch(this);
            _dimmerSwitch = new OpenZWaveDimmerSwitch(this);
            _thermostat = new OpenZWaveThermostat(this);
            _binarySensor = new OpenZWaveBinarySensor(this);
            _powerSensor = new OpenZWavePowerSensor(this);
        }

        public void OptimizePaths()
        {
            //TODO: remove the need for this cast
            var homeId = ((OpenZWaveNetwork) Network).HomeId.Value;
            Manager.HealNetworkNode(homeId, Id, true);
        }

        internal void RemoveValue(OpenZWaveDeviceValue value)
        {
            var remove = Values.Match(value.DeviceId, value.CommandClass, value.Index);

            Values.Remove(remove);
        }

        internal bool ProcessValueChanged(OpenZWaveDeviceValue value)
        {
            if (_toggleSwitch.ProcessValueChanged(value))
            {
                return true;
            }

            if (_dimmerSwitch.ProcessValueChanged(value))
            {
                return true;
            }

            if (_powerSensor.ProcessValueChanged(value))
            {
                return true;
            }

            if (_thermostat.ProcessValueChanged(value))
            {
                return true;
            }

            return false;
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

        public override IMultilevelSensor<IPower> PowerSensor
        {
            get
            {
                return _powerSensor;
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

        public override string ToString()
        {
            return this.FormatData();
        }
    }
}
