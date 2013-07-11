using Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Exceptions;
using Roomie.Common.ScriptingLanguage;
using System.Collections.Generic;
using System.Linq;
using BaseDevice = Roomie.Common.HomeAutomation.Device;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public abstract class Device : BaseDevice
    {
        //TODO: Create LastPolled dealy?
        //TODO: add public access for Network

        public List<DeviceEventAction> DeviceEventActions { get; private set; }

        public DeviceLocation Location
        {
            get
            {
                return base.location;
            }
            set
            {
                base.location = value;
            }
        }

        private Network Network
        {
            get
            {
                //TODO: this isn't so great
                return network as Network;
            }
        }

        private HomeAutomationNetworkContext Context
        {
            get
            {
                return Network.Context;
            }
        }

        protected Device(Network network, int maxPower, DeviceType type = null, string name = null, DeviceLocation location = null)
            :base(location??new DeviceLocation(), network)
        {
            this.network = network;
            this.MaxPower = maxPower;
            this.Type = type??DeviceType.Unknown;
            this.Name = name;
            this.power = null;
            this.IsConnected = null;

            DeviceEventActions = new List<DeviceEventAction>();
        }

        protected abstract int? SetPower(int power);
        public abstract void Poll();

        public override int? Power
        {
            get
            {
                return base.power;
            }
            set
            {
                if (value.Value < 0)
                    throw new HomeAutomationException("Power must be greater than or equal to 0 (attempted value is " + value.Value + ")");

                if (value.Value > MaxPower)
                    value = MaxPower;

                SetPower(value.Value);
            }
        }

        //TODO: min and max power values

        protected void PowerChanged()
        {
            var threadPool = Context.ThreadPool;

            //TODO: print based on selected IEvent
            threadPool.Print(BuildVirtualAddress(false, false) + " power level changed to " + Power);

            //TODO: improve this logic
            //TODO: include previous power for more smarts
            IEvent @event;
            IEventSource source = null; //TODO: fill this in
            if (IsConnected == true)
            {
                if (Type.Equals(DeviceType.MotionDetector))
                {
                    if (ToggleSwitch.IsOn)
                    {
                        @event = DeviceEvent.MotionDetected(this, source);
                    }
                    else if (ToggleSwitch.IsOff)
                    {
                        @event = DeviceEvent.StillnessDetected(this, source);
                    }
                    else
                    {
                        @event = DeviceEvent.PowerChanged(this, source);
                    }
                }
                else if (Type.Equals(DeviceType.Thermostat))
                {
                    @event = DeviceEvent.TemperatureChanged(this, source);
                }
                else
                {
                    @event = DeviceEvent.PowerChanged(this, source);
                }
            }
            else
            {
                @event = DeviceEvent.Lost(this, source);
            }

            Context.History.Add(@event);

            var eventActions = DeviceEventActions.Where(a => a.Matches(DeviceEventType.PowerChange));

            if (ToggleSwitch.IsOn)
            {
                var onScripts = DeviceEventActions.Where(a => a.Matches(DeviceEventType.PowerOn));
                eventActions = eventActions.Union(onScripts);
            }

            if (ToggleSwitch.IsOff)
            {
                var onScripts = DeviceEventActions.Where(a => a.Matches(DeviceEventType.PowerOff));
                eventActions = eventActions.Union(onScripts);
            }

            var scripts = eventActions.Select(a => a.Commands);

            if (Context.WebHookPresent)
            {
                var syncScript = new ScriptCommandList();
                syncScript.Add(Context.SyncWithCloudCommand);
                scripts = new[] {syncScript}.Union(scripts);
            }

            foreach (var script in scripts)
            {
                threadPool.AddCommands(script);
            }
        }

        public override string ToString()
        {
            return BuildVirtualAddress(true, false);
        }

    }
}
