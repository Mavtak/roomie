using System.Collections.Generic;
using System.Linq;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.DimmerSwitches;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.ToggleSwitches;
using Roomie.Common.ScriptingLanguage;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public abstract class Device : IDevice
    {
        //TODO: Create LastPolled dealy?
        //TODO: add public access for Network

        public List<DeviceEventAction> DeviceEventActions { get; private set; }

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

            DeviceEventActions = new List<DeviceEventAction>();
        }

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
                        case ToggleSwitchPower.On:
                            @event = DeviceEvent.MotionDetected(this, source);
                            break;

                        case ToggleSwitchPower.Off:
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
                            case ToggleSwitchPower.On:
                                @event = DeviceEvent.PoweredOn(this, source);
                                break;

                            case ToggleSwitchPower.Off:
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

            //TODO: add an event trigger for DeviceStateChanged

            var eventActions = DeviceEventActions.Where(a => a.Matches(DeviceEventType.PowerChange));


            switch (ToggleSwitch.Power)
            {
                case ToggleSwitchPower.On:
                    var onScripts = DeviceEventActions.Where(a => a.Matches(DeviceEventType.PowerOn));
                    eventActions = eventActions.Union(onScripts);
                    break;

                case ToggleSwitchPower.Off:
                    var offScripts = DeviceEventActions.Where(a => a.Matches(DeviceEventType.PowerOff));
                    eventActions = eventActions.Union(offScripts);
                    break;
            }

            var scripts = eventActions.Select(a => a.Commands);

            if (Context.WebHookPresent)
            {
                var syncScript = new ScriptCommandList();
                syncScript.Add(Context.SyncWithCloudCommand);
                scripts = new[] { syncScript }.Union(scripts);
            }

            foreach (var script in scripts)
            {
                threadPool.AddCommands(script);
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

        public abstract IToggleSwitch ToggleSwitch { get; }
        public abstract IDimmerSwitch DimmerSwitch { get; }
        public abstract IThermostat Thermostat { get; }

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

        IToggleSwitchState IDeviceState.ToggleSwitchState
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

        #endregion

        #region IDeviceActions

        IToggleSwitchActions IDeviceActions.ToggleSwitchActions
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

        #endregion

    }
}
