using Roomie.Common.HomeAutomation;
using Roomie.CommandDefinitions.HomeAutomationCommands.Exceptions;
using BaseDevice = Roomie.Common.HomeAutomation.Device;
using Roomie.Common.HomeAutomation.Exceptions;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public abstract class Device : BaseDevice
    {
        //TODO: Create PowerChanged event?
        //TODO: Create LastPolled dealy?
        //TODO: add public access for Network

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

        protected Device(Network network, int maxPower, DeviceType type = null, string name = null, DeviceLocation location = null)
            :base(location??new DeviceLocation(), network)
        {
            this.network = network;
            this.MaxPower = maxPower;
            this.Type = type??DeviceType.Unknown;
            this.Name = name;
            this.power = null;
            this.IsConnected = null;
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
            }
        }


        //TODO: min and max power values

        

        public override string ToString()
        {
            return this.BuildAddress();
        }

    }
}
