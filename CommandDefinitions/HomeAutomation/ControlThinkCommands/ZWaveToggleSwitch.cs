using System;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.ToggleSwitches;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    public class ZWaveToggleSwitch : IToggleSwitch
    {
        private ZWaveDevice _device;

        public ZWaveToggleSwitch(ZWaveDevice device)
        {
            _device = device;
        }

        public void PowerOn()
        {
            _device.DoDeviceOperation(() =>
            {
                _device.BackingObject.PowerOn();
                _device.IsConnected = true;
            });
        }
        public void PowerOff()
        {
            _device.DoDeviceOperation(() =>
            {
                _device.BackingObject.PowerOff();
                _device.IsConnected = true;
            });
        }

        public void Poll()
        {
            _device.DimmerSwitch.Poll();
        }

        public ToggleSwitchPower? Power
        {
            get
            {
                var power =_device.DimmerSwitch.Power;

                if (Utilities.IsOn(power))
                {
                    return ToggleSwitchPower.On;
                }

                if (Utilities.IsOff(power))
                {
                    return ToggleSwitchPower.Off;
                }

                return null;
            }
        }
    }
}
