using System.Collections.Generic;
using PIEHidDotNet;
using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.BinarySensors;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.Keypads.Buttons;
using Roomie.Common.HomeAutomation.MultilevelSensors;
using Roomie.Common.HomeAutomation.MultilevelSwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.Measurements.Humidity;
using Roomie.Common.Measurements.Power;
using Roomie.Common.Measurements.Temperature;

namespace Roomie.CommandDefinitions.PiEngineeringCommands
{
    public class PiEngineeringDevice : Device
    {
        public IEnumerable<IKeypadButtonState> Buttons { get; private set; }

        private readonly PiEngineeringKeypad _keypad;
        internal PIEDevice BackingObject { get; private set; }

        public PiEngineeringDevice(Network network, PIEDevice device, string name = null, ILocation location = null)
            : base(network, DeviceType.Keypad, name, location)
        {
            BackingObject = device;
            IsConnected = true;
            _keypad = new PiEngineeringKeypad(this);
        }

        #region Device overrides

        public override IBinarySwitch BinarySwitch
        {
            get
            {
                return null;
            }
        }

        public override IMultilevelSwitch MultilevelSwitch
        {
            get
            {
                return null;
            }
        }

        public override IBinarySensor BinarySensor
        {
            get
            {
                return null;
            }
        }

        public override IMultilevelSensor<IPower> PowerSensor
        {
            get
            {
                return null;
            }
        }

        public override IMultilevelSensor<ITemperature> TemperatureSensor
        {
            get
            {
                return null;
            }
        }

        public override IMultilevelSensor<IHumidity> HumiditySensor
        {
            get
            {
                return null;
            }
        }

        public override IThermostat Thermostat
        {
            get
            {
                return null;
            }
        }

        public override IKeypad Keypad
        {
            get
            {
                return _keypad;
            }
        }

        #endregion
    }
}
