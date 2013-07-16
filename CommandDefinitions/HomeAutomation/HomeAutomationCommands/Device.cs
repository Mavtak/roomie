using System.Collections.Generic;
using System.Linq;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.ToggleSwitches;
using Roomie.Common.ScriptingLanguage;
using BaseDevice = Roomie.Common.HomeAutomation.Device;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public abstract class Device : BaseDevice
    {
        //TODO: Create LastPolled dealy?
        //TODO: add public access for Network

        public List<DeviceEventAction> DeviceEventActions { get; private set; }

        private new Network Network
        {
            get
            {
                //TODO: this isn't so great
                return base.Network as Network;
            }
        }

        private HomeAutomationNetworkContext Context
        {
            get
            {
                return Network.Context;
            }
        }

        protected Device(Network network, DeviceType type = null, string name = null, DeviceLocation location = null)
            :base(location??new DeviceLocation(), network)
        {
            base.Network = network;
            this.Type = type??DeviceType.Unknown;
            this.Name = name;
            this.IsConnected = null;

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

    }
}
