using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.BinarySwitches;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    internal class ZWaveToggleSwitch : IBinarySwitch
    {
        private readonly ZWaveDevice _device;

        public ZWaveToggleSwitch(ZWaveDevice device)
        {
            _device = device;
        }

        public void PowerOn()
        {
            _device.DoDeviceOperation(_device.BackingObject.PowerOn);
        }
        public void PowerOff()
        {
            _device.DoDeviceOperation(_device.BackingObject.PowerOff);
        }

        public void Poll()
        {
            _device.DimmerSwitch.Poll();
        }

        public BinarySwitchPower? Power
        {
            get
            {
                var power =_device.DimmerSwitch.Power;

                if (Utilities.IsOn(power))
                {
                    return BinarySwitchPower.On;
                }

                if (Utilities.IsOff(power))
                {
                    return BinarySwitchPower.Off;
                }

                return null;
            }
        }
    }
}
