using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.Exceptions;
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

                base.power = SetPower(value.Value);

                PowerChanged();
            }
        }

        //TODO: min and max power values

        protected void PowerChanged()
        {
            var threadPool = Network.ThreadPool;

            threadPool.Print(BuildVirtualAddress(false, false) + " power level changed to " + Power);

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

            foreach (var script in eventActions.Select(a => a.Commands))
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
