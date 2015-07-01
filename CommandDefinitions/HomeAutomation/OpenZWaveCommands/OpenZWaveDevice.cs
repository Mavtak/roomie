using OpenZWaveDotNet;
using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Common.HomeAutomation.BinarySensors;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.MultilevelSensors;
using Roomie.Common.HomeAutomation.MultilevelSwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.Measurements.Humidity;
using Roomie.Common.Measurements.Illuminance;
using Roomie.Common.Measurements.Power;
using Roomie.Common.Measurements.Temperature;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWaveDevice : Device
    {
        internal OpenZWaveDeviceValueCollection Values { get; private set; }
        internal ZWManager Manager { get; private set; }
        internal byte Id { get; private set; }

        public OpenZWaveEvent Event { get; private set; }

        private readonly OpenZWaveToggleSwitch _toggleSwitch;
        private readonly OpenZWaveDimmerSwitch _dimmerSwitch;
        private readonly OpenZWaveThermostat _thermostat;
        private readonly OpenZWaveBinarySensor _binarySensor;
        private readonly OpenZWaveTemperatureSensor _temperatureSensor;
        private readonly OpenZWavePowerSensor _powerSensor;
        private readonly OpenZWaveHumiditySensor _humiditySensor;
        private readonly OpenZWaveIlluminanceSensor _illuminanceSensor;

        public OpenZWaveDevice(Network network, ZWManager manager, byte id)
            : base(network)
        {
            Manager = manager;
            Id = id;
            Values = new OpenZWaveDeviceValueCollection();

            Address = Id.ToString();

            Event = new OpenZWaveEvent(this);

            _toggleSwitch = new OpenZWaveToggleSwitch(this);
            _dimmerSwitch = new OpenZWaveDimmerSwitch(this);
            _thermostat = new OpenZWaveThermostat(this);
            _binarySensor = new OpenZWaveBinarySensor(this);
            _powerSensor = new OpenZWavePowerSensor(this);
            _temperatureSensor = new OpenZWaveTemperatureSensor(this);
            _humiditySensor = new OpenZWaveHumiditySensor(this);
            _illuminanceSensor = new OpenZWaveIlluminanceSensor(this);
        }

        public void OptimizePaths(bool returnRouteOptimization)
        {
            //TODO: remove the need for this cast
            var network = ((OpenZWaveNetwork)Network);
            var homeId = network.HomeId.Value;

            using (var stateWatcher = new ControllerStateWatcher(network))
            {
                Manager.HealNetworkNode(homeId, Id, returnRouteOptimization);

                //TODO: figure out final state
                stateWatcher.LogChangesForever();
            }
        }

        internal bool ProcessValueUpdate(OpenZWaveDeviceValue value, ValueUpdateType updateType)
        {
            if (_toggleSwitch.ProcessValueUpdate(value, updateType))
            {
                return true;
            }

            if (_dimmerSwitch.ProcessValueUpdate(value, updateType))
            {
                return true;
            }

            if (_binarySensor.ProcessValueUpdate(value, updateType))
            {
                return true;
            }

            if (_powerSensor.ProcessValueUpdate(value, updateType))
            {
                return true;
            }

            if (_humiditySensor.ProcessValueUpdate(value, updateType))
            {
                return true;
            }

            if (_illuminanceSensor.ProcessValueUpdate(value, updateType))
            {
                return true;
            }

            if (_temperatureSensor.ProcessValueUpdate(value, updateType))
            {
                return true;
            }

            if (_thermostat.ProcessValueUpdate(value, updateType))
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

        public override IMultilevelSensor<ITemperature> TemperatureSensor
        {
            get
            {
                return _temperatureSensor;
            }
        }

        public override IMultilevelSensor<IHumidity> HumiditySensor
        {
            get
            {
                return _humiditySensor;
            }
        }

        public override IMultilevelSensor<IIlluminance> IlluminanceSensor
        {
            get
            {
                return _illuminanceSensor;
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
