using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
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

namespace Roomie.Web.Persistence.Models
{
    public class DeviceModel : IDevice, IHasDivId
    {
        [Key]
        public int Id { get; set; }

        public bool? IsConnected { get; set; }
        public DeviceType Type { get; set; }

        public string CurrentAction { get; set; }

        public virtual NetworkModel Network { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual DeviceLocationModel Location { get; set; }

        private string _notes;
        private bool _deserializedFromDatabase;
        public string Notes
        {
            get
            {
                return _notes;
            }
            set
            {
                if (!_deserializedFromDatabase && !string.Equals(_notes, value))
                {
                    var element = XElement.Parse(value);
                    var state = element.ToDeviceState();

                    Update(state, true);

                    _deserializedFromDatabase = true;
                }

                _notes = value;
            }
        }

        public void UpdateSerializedValue()
        {
            Notes = this.ToXElement().ToString();
        }

        public void Poll()
        {
            this.DoCommand("PollDevice");
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

                if (Location == null && state.Location != null && state.Location.IsSet)
                {
                    Location = new DeviceLocationModel
                    {
                        Name = string.Join("/", state.Location.GetParts())
                    };
                }
            }

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

        private readonly ToggleSwitchModel _binarySwitch;
        private readonly DimmerSwitchModel _multilevelSwitch;
        private readonly ColorSwitchModel _colorSwitch;
        private readonly BinarySensorModel _binarySensor;
        private readonly MultilevelSensorModel<IPower> _powerSensor;
        private readonly MultilevelSensorModel<ITemperature> _temperatureSensor;
        private readonly MultilevelSensorModel<IHumidity> _humiditySensor;
        private readonly MultilevelSensorModel<IIlluminance> _illuminanceSensor;
        private readonly ThermostatModel _thermostat;

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

        //TODO: implement
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

        public DeviceModel()
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

        #region LastPing

        public DateTime? LastPing { get; set; }

        public TimeSpan? TimeSinceLastPing
        {
            get
            {
                if (LastPing == null)
                    return null;
                return DateTime.UtcNow.Subtract(LastPing.Value);
            }
        }

        public void UpdatePing()
        {
            LastPing = DateTime.UtcNow;
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

        #region Object overrides

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var that = obj as DeviceModel;

            if (obj == null)
            {
                return false;
            }

            return this.Equals(that);
        }

        public bool Equals(DeviceModel that)
        {
            if (that == null)
            {
                return false;
            }

            if (this.Address != that.Address)
            {
                return false;
            }

            if (!NetworkModel.Equals(this.Network, that.Network))
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        public override string ToString()
        {
            var builder = new System.Text.StringBuilder();

            builder.Append(this.BuildVirtualAddress(false, false));
            builder.Append(" - ");
            builder.Append(this.Describe());

            return builder.ToString();
        }

        #endregion

        #region HasId implementation

        public string DivId
        {
            get
            {
                return "device" + Id;
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

        #endregion
    }
}