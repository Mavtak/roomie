using System.Collections.Generic;
using System.Linq;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.DimmerSwitches;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.ScriptingLanguage;
using Roomie.Common.Triggers;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public abstract class Device : IDevice
    {
        //TODO: Create LastPolled dealy?
        //TODO: add public access for Network
        public Network Network { get; private set; }

        private HomeAutomationNetworkContext Context
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
        }

        protected void PowerChanged()
        {
            var threadPool = Context.ThreadPool;

            //TODO: improve this logic
            //TODO: read from event history in making powered on/off decision
            IDeviceEvent @event = null;
            IEventSource source = null; //TODO: fill this in
            if (IsConnected == true)
            {
                if (Type.Equals(DeviceType.MotionDetector))
                {
                    switch (ToggleSwitch.Power)
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
                    if (Type.Equals(DeviceType.Switch))
                    {
                        switch (ToggleSwitch.Power)
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

        public abstract IBinarySwitch ToggleSwitch { get; }
        public abstract IDimmerSwitch DimmerSwitch { get; }
        public abstract IThermostat Thermostat { get; }
        public abstract IKeypad Keypad { get; }

        public void Update(IDeviceState state)
        {
            Name = state.Name;
            Address = state.Address;
            Location.Update(state.Location);
            IsConnected = state.IsConnected;
            Type = state.Type;
            DimmerSwitch.Update(state.DimmerSwitchState);
            //TODO: handle thermostat state and such
        }

        #endregion

        #region IDeviceState

        
        public string Name { get; set; }
        public string Address { get; set; }
        public ILocation Location { get; private set; }
        public DeviceType Type { get; set; }
        public bool? IsConnected { get; set; }

        INetworkState IDeviceState.NetworkState
        {
            get
            {
                return Network;
            }
        }

        IBinarySwitchState IDeviceState.ToggleSwitchState
        {
            get
            {
                return ToggleSwitch;
            }
        }
        IDimmerSwitchState IDeviceState.DimmerSwitchState
        {
            get
            {
                return DimmerSwitch;
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
            if (Type == DeviceType.Switch)
            {
                ToggleSwitch.Poll();
                return;
            }

            if (Type == DeviceType.Dimmable)
            {
                DimmerSwitch.Poll();
                return;
            }

            if (Type == DeviceType.Thermostat)
            {
                Thermostat.PollTemperature();

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

        IBinarySwitchActions IDeviceActions.ToggleSwitchActions
        {
            get
            {
                return ToggleSwitch;
            }
        }

        IDimmerSwitchActions IDeviceActions.DimmerSwitchActions
        {
            get
            {
                return DimmerSwitch;
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
