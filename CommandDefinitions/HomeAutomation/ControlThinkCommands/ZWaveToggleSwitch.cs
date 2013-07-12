using System;
using Roomie.Common.HomeAutomation;

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
            Action operation = () =>
            {
                _device.BackingObject.PowerOn();
                _device.IsConnected = true;
            };

            _device.DoDeviceOperation(operation);
        }
        public void PowerOff()
        {
            Action operation = () =>
            {
                _device.BackingObject.PowerOff();
                _device.IsConnected = true;
            };

            _device.DoDeviceOperation(operation);
        }

        public void Poll()
        {
            _device.DimmerSwitch.Poll();
        }

        public bool IsOff
        {
            get
            {
                return Utilities.IsOff(_device.DimmerSwitch.Power);
            }
        }

        public bool IsOn
        {
            get
            {
                return Utilities.IsOn(_device.DimmerSwitch.Power);
            }
        }
    }
}
