﻿using Roomie.Common.HomeAutomation;
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

        public void SetPower(BinarySwitchPower power)
        {
            switch (power)
            {
                case BinarySwitchPower.On:
                    _device.DoDeviceOperation(_device.BackingObject.PowerOn);
                    break;

                case BinarySwitchPower.Off:
                    _device.DoDeviceOperation(_device.BackingObject.PowerOff);
                    break;
            }
        }

        public void Poll()
        {
            _device.MultilevelSwitch.Poll();
        }

        public BinarySwitchPower? Power
        {
            get
            {
                var power = _device.MultilevelSwitch.Power;

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
