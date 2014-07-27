using System.Collections.Generic;
using System.Linq;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.BinarySensors;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.MultilevelSensors;
using Roomie.Common.HomeAutomation.MultilevelSwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.Measurements.Humidity;
using Roomie.Common.Measurements.Illuminance;
using Roomie.Common.Measurements.Power;
using Roomie.Common.Measurements.Temperature;
using Roomie.Common.ScriptingLanguage;
using Roomie.Common.Triggers;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public abstract class Device : IDevice
    {
        //TODO: Create LastPolled dealy?
        //TODO: add public access for Network
        public Network Network { get; private set; }

        protected HomeAutomationNetworkContext Context
        {
            get
            {
                return Network.Context;
            }
        }

        protected Device(Network network, DeviceType type = null, string name = null, ILocation location = null)
        {
            Network = network;
            Location = location ?? new Location();
            Type = type ?? DeviceType.Unknown;
            Name = name;
            IsConnected = null;
            CurrentStateGenerator = new CurrentStateGenerator();
        }

        public void AddEvent(IDeviceEvent @event)
        {
            var threadPool = Context.ThreadPool;

            var message = this.BuildVirtualAddress(false, false) + " " + @event.Type.Name + ". Current State: " + @event.State.Describe();

            threadPool.Print(message);

            Context.History.Add(@event);
            Context.Triggers.CheckAndAct();

            if (Context.WebHookPresent)
            {
                var syncScript = new ScriptCommandList();
                syncScript.Add(Context.SyncWithCloudCommand(Network));
                threadPool.AddCommands(syncScript);
            }
        }

        public override string ToString()
        {
            return this.BuildVirtualAddress(true, false);
        }


        #region IDevice

        INetwork IDevice.Network
        {
            get
            {
                return Network;
            }
        }

        public abstract IBinarySwitch BinarySwitch { get; }
        public abstract IMultilevelSwitch MultilevelSwitch { get; }
        public abstract IBinarySensor BinarySensor { get; }
        public abstract IMultilevelSensor<IPower> PowerSensor { get; }
        public abstract IMultilevelSensor<ITemperature> TemperatureSensor { get; }
        public abstract IMultilevelSensor<IHumidity> HumiditySensor { get; }
        public abstract IMultilevelSensor<IIlluminance> IlluminanceSensor { get; }
        public abstract IThermostat Thermostat { get; }
        public abstract IKeypad Keypad { get; }

        public void Update(IDeviceState state)
        {
            Name = state.Name;
            Address = state.Address;
            Location.Update(state.Location);
            IsConnected = state.IsConnected;
            Type = state.Type;

            //TODO: update subdevices?
        }

        #endregion

        #region IDeviceState

        
        public string Name { get; set; }
        public string Address { get; set; }
        public ILocation Location { get; private set; }
        public DeviceType Type { get; set; }
        public string CurrentAction { get; set; }
        public CurrentStateGenerator CurrentStateGenerator { get; private set; }
        public bool? IsConnected { get; set; }

        public void UpdateCurrentAction()
        {
            var newCurrentAction = CurrentStateGenerator.Generate(BinarySwitch.Power, PowerSensor.Value);

            if (newCurrentAction != CurrentAction)
            {
                CurrentAction = newCurrentAction;

                var @event = DeviceEvent.CurrentActionChanged(this, null);
                AddEvent(@event);
            }
        }

        INetworkState IDeviceState.NetworkState
        {
            get
            {
                return Network;
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

        #region IDeviceActions

        public void Poll()
        {
            if (Type == DeviceType.BinarySwitch)
            {
                BinarySwitch.Poll();
                return;
            }

            if (Type == DeviceType.MultilevelSwitch)
            {
                MultilevelSwitch.Poll();
                return;
            }

            if (Type == DeviceType.Thermostat)
            {
                if (TemperatureSensor != null)
                {
                    TemperatureSensor.Poll();
                }

                if (Thermostat.Core != null)
                {
                    Thermostat.Core.PollCurrentAction();
                    Thermostat.Core.PollMode();
                    Thermostat.Core.PollSupportedModes();
                }

                if (Thermostat.Fan != null)
                {
                    Thermostat.Fan.PollCurrentAction();
                    Thermostat.Fan.PollMode();
                    Thermostat.Fan.PollSupportedModes();
                }

                if (Thermostat.Setpoints != null)
                {
                    Thermostat.Setpoints.PollSupportedSetpoints();
                    Thermostat.Setpoints.PollSetpointTemperatures();
                }
            }
        }

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
