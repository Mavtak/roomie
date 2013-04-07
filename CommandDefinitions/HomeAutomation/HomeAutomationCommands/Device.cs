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

        public abstract void PowerOn();
        public abstract void PowerOff();
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

            threadPool.Print(BuildVirtualAddress(false, false) + " power level changed to " + Power);

            //TODO: improve this logic
            //TODO: include previous power for more smarts
            if (IsConnected == true)
            {
                Context.History.Add(DeviceEvent.PowerChanged(this, null));
            }
            else
            {
                Context.History.Add(DeviceEvent.Lost(this, null));
            }

            var eventActions = DeviceEventActions.Where(a => a.Matches(DeviceEventType.PowerChange));

            if (IsOn)
            {
                var onScripts = DeviceEventActions.Where(a => a.Matches(DeviceEventType.PowerOn));
                eventActions = eventActions.Union(onScripts);
            }

            if (IsOff)
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
