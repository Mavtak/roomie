using System;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.BinarySensors;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.ColorSwitch;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.MultilevelSensors;
using Roomie.Common.HomeAutomation.MultilevelSwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.Measurements.Humidity;
using Roomie.Common.Measurements.Illuminance;
using Roomie.Common.Measurements.Power;
using Roomie.Common.Measurements.Temperature;
using Roomie.Web.Persistence.Helpers;
using Roomie.Web.Persistence.Repositories;

namespace Roomie.Web.Persistence.Models
{
    public class Device : IDevice
    {
        public string Address { get; private set; }
        public string CurrentAction { get; private set; }
        public int Id { get; private set; }
        public bool? IsConnected { get; private set; }
        public DateTime? LastPing { get; private set; }
        public virtual ILocation Location { get; private set; }
        public string Name { get; private set; }
        public virtual Network Network { get; private set; }
        public DeviceType Type { get; private set; }

        public IScriptRepository ScriptRepository { get; private set; }
        public ITaskRepository TaskRepository { get; private set; }

        private readonly ToggleSwitchModel _binarySwitch;
        private readonly DimmerSwitchModel _multilevelSwitch;
        private readonly ColorSwitchModel _colorSwitch;
        private readonly BinarySensorModel _binarySensor;
        private readonly MultilevelSensorModel<IPower> _powerSensor;
        private readonly MultilevelSensorModel<ITemperature> _temperatureSensor;
        private readonly MultilevelSensorModel<IHumidity> _humiditySensor;
        private readonly MultilevelSensorModel<IIlluminance> _illuminanceSensor;
        private readonly ThermostatModel _thermostat;

        #region device models

        public ToggleSwitchModel BinarySwitch
        {
            get
            {
                return _binarySwitch;
            }
        }

        public DimmerSwitchModel MultilevelSwitch
        {
            get
            {
                return _multilevelSwitch;
            }
        }

        public ColorSwitchModel ColorSwitch
        {
            get
            {
                return _colorSwitch;
            }
        }

        public BinarySensorModel BinarySensor
        {
            get
            {
                return _binarySensor;
            }
        }

        public MultilevelSensorModel<IPower> PowerSensor
        {
            get
            {
                return _powerSensor;
            }
        }

        public MultilevelSensorModel<ITemperature> TemperatureSensor
        {
            get
            {
                return _temperatureSensor;
            }
        }

        public MultilevelSensorModel<IHumidity> HumiditySensor
        {
            get
            {
                return _humiditySensor;
            }
        }

        public MultilevelSensorModel<IIlluminance> IlluminanceSensor
        {
            get
            {
                return _illuminanceSensor;
            }
        }

        public ThermostatModel Thermostat
        {
            get
            {
                return _thermostat;
            }
        }

        //TODO: implement this
        public IKeypad Keypad
        {
            get
            {
                return null;
            }
        }

        #endregion

        public bool IsAvailable
        {
            get
            {
                return (IsConnected == true)
                    && (Network != null)
                    && (Network.IsAvailable == true);
            }
        }

        public TimeSpan? TimeSinceLastPing
        {
            get
            {
                if (LastPing == null)
                    return null;
                return DateTime.UtcNow.Subtract(LastPing.Value);
            }
        }

        public Device()
        {
            _binarySwitch = new ToggleSwitchModel(this);
            _multilevelSwitch = new DimmerSwitchModel(this);
            _colorSwitch = new ColorSwitchModel(this);
            _thermostat = new ThermostatModel(this);
            _binarySensor = new BinarySensorModel(this);
            _powerSensor = new MultilevelSensorModel<IPower>(this);
            _temperatureSensor = new MultilevelSensorModel<ITemperature>(this);
            _humiditySensor = new MultilevelSensorModel<IHumidity>(this);
            _illuminanceSensor = new MultilevelSensorModel<IIlluminance>(this);
        }

        public Device(string address, DateTime? lastPing, int id, string name, Network network, IScriptRepository scripts, IDeviceState state, ITaskRepository tasks, DeviceType type)
            : this()
        {
            Address = address;
            LastPing = lastPing;
            Id = id;
            Name = name;
            Network = network;
            ScriptRepository = scripts;
            TaskRepository = tasks;
            Type = type;

            if (state != null)
            {
                Update(state, false);
            }
        }

        public static Device Create(string address, bool? isConnected, string name, Network network, ILocation location, DeviceType type, string currentAction = null)
        {
            var result = new Device(
                address: address,
                lastPing: null,
                id: 0,
                name: name,
                network: network,
                scripts: null,
                state: null,
                tasks: null,
                type: type
                );

            result.CurrentAction = currentAction;

            return result;
        }

        public static Device Create(int id, IDeviceState state)
        {
            var result = new Device(
                address: null,
                lastPing: null,
                id: id,
                name: null,
                network: null,
                scripts: null,
                state: state,
                tasks: null,
                type: null
                );

            return result;
        }

        public void Update(IDeviceState state, bool fromDatabase = false)
        {
            //TODO: update more properties?

            if (!fromDatabase)
            {
                if (Name == null && state.Name != null)
                {
                    Name = state.Name;
                }

                if ((Type == null || Type.Equals(DeviceType.Unknown)) && state.Type != null)
                {
                    Type = state.Type;
                }
            }

            Location = (state.Location == null) ? null : new Location(state.Location.GetParts());

            IsConnected = state.IsConnected;
            CurrentAction = state.CurrentAction;

            BinarySwitch.Update(state.BinarySwitchState ?? ReadOnlyBinarySwitchSwitchState.Blank());
            MultilevelSwitch.Update(state.MultilevelSwitchState ?? ReadOnlyMultilevelSwitchState.Blank());
            ColorSwitch.Update(state.ColorSwitchState ?? ReadOnlyColorSwitchState.Blank());
            BinarySensor.Update(state.BinarySensorState ?? ReadOnlyBinarySensorState.Blank());
            PowerSensor.Update(state.PowerSensorState ?? ReadOnlyMultilevelSensorState<IPower>.Blank());
            TemperatureSensor.Update(state.TemperatureSensorState ?? ReadOnlyMultilevelSensorState<ITemperature>.Blank());
            HumiditySensor.Update(state.HumiditySensorState ?? ReadOnlyMultilevelSensorState<IHumidity>.Blank());
            IlluminanceSensor.Update(state.IlluminanceSensorState ?? ReadOnlyMultilevelSensorState<IIlluminance>.Blank());
            Thermostat.Update(state.ThermostatState ?? ReadOnlyThermostatState.Blank());
        }

        public void Update(ILocation location, string name, string type)
        {
            Location = location;
            Name = name;
            Type = type;
        }

        public void UpdatePing()
        {
            LastPing = DateTime.UtcNow;
        }

        public void SetId(int id)
        {
            if (Id != 0)
            {
                throw new ArgumentException("Id is already set");
            }

            Id = id;
        }

        #region IDevice

        INetwork IDevice.Network
        {
            get
            {
                return Network;
            }
        }

        IBinarySwitch IDevice.BinarySwitch
        {
            get
            {
                return BinarySwitch;
            }
        }

        IMultilevelSwitch IDevice.MultilevelSwitch
        {
            get
            {
                return MultilevelSwitch;
            }
        }

        IColorSwitch IDevice.ColorSwitch
        {
            get
            {
                return ColorSwitch;
            }
        }

        IBinarySensor IDevice.BinarySensor
        {
            get
            {
                //TODO: implement
                return null;
            }
        }

        IMultilevelSensor<IPower> IDevice.PowerSensor
        {
            get
            {
                return PowerSensor;
            }
        }

        IMultilevelSensor<ITemperature> IDevice.TemperatureSensor
        {
            get
            {
                return TemperatureSensor;
            }
        }

        IMultilevelSensor<IHumidity> IDevice.HumiditySensor
        {
            get
            {
                return HumiditySensor;
            }
        }

        IMultilevelSensor<IIlluminance> IDevice.IlluminanceSensor
        {
            get
            {
                return IlluminanceSensor;
            }
        }

        IThermostat IDevice.Thermostat
        {
            get
            {
                return Thermostat;
            }
        }

        IKeypad IDevice.Keypad
        {
            get
            {
                return Keypad;
            }
        }

        #endregion

        #region IDeviceActions

        IBinarySwitchActions IDeviceActions.BinarySwitchActions
        {
            get
            {
                return BinarySwitch;
            }
        }

        IMultilevelSwitchActions IDeviceActions.MultilevelSwitchActions
        {
            get
            {
                return MultilevelSwitch;
            }
        }

        IColorSwitchActions IDeviceActions.ColorSwitchActions
        {
            get
            {
                return ColorSwitch;
            }
        }
        public IBinarySensorActions BinarySensorActions
        {
            get
            {
                return BinarySensor;
            }
        }

        public IMultilevelSensorActions PowerSensorActions
        {
            get
            {
                return PowerSensor;
            }
        }

        public IMultilevelSensorActions TemperatureSensorActions
        {
            get
            {
                return TemperatureSensor;
            }
        }

        public IMultilevelSensorActions HumiditySensorActions
        {
            get
            {
                return HumiditySensor;
            }
        }

        public IMultilevelSensorActions IlluminanceSensorActions
        {
            get
            {
                return IlluminanceSensor;
            }
        }

        IThermostatActions IDeviceActions.ThermostatActions
        {
            get
            {
                return Thermostat;
            }
        }

        IKeypadActions IDeviceActions.KeypadActions
        {
            get
            {
                return Keypad;
            }
        }

        public void Poll()
        {
            this.DoCommand("PollDevice");
        }

        #endregion

        #region IDeviceState

        INetworkState IDeviceState.NetworkState
        {
            get
            {
                return Network;
            }
        }

        ILocation IDeviceState.Location
        {
            get
            {
                return Location;
            }
        }

        IBinarySwitchState IDeviceState.BinarySwitchState
        {
            get
            {
                return BinarySwitch;
            }
        }

        IMultilevelSwitchState IDeviceState.MultilevelSwitchState
        {
            get
            {
                return MultilevelSwitch;
            }
        }

        IColorSwitchState IDeviceState.ColorSwitchState
        {
            get
            {
                return ColorSwitch;
            }
        }

        public IBinarySensorState BinarySensorState
        {
            get
            {
                return BinarySensor;
            }
        }

        public IMultilevelSensorState<IPower> PowerSensorState
        {
            get
            {
                return PowerSensor;
            }
        }

        public IMultilevelSensorState<ITemperature> TemperatureSensorState
        {
            get
            {
                return TemperatureSensor;
            }
        }

        public IMultilevelSensorState<IHumidity> HumiditySensorState
        {
            get
            {
                return HumiditySensor;
            }
        }

        public IMultilevelSensorState<IIlluminance> IlluminanceSensorState
        {
            get
            {
                return IlluminanceSensor;
            }
        }

        IThermostatState IDeviceState.ThermostatState
        {
            get
            {
                return Thermostat;
            }
        }

        IKeypadState IDeviceState.KeypadState
        {
            get
            {
                return Keypad;
            }
        }

        #endregion

        #region object overrides

        public override string ToString()
        {
            var builder = new System.Text.StringBuilder();

            builder.Append(this.BuildVirtualAddress(false, false));
            builder.Append(" - ");
            builder.Append(this.Describe());

            return builder.ToString();
        }

        #endregion
    }
}
