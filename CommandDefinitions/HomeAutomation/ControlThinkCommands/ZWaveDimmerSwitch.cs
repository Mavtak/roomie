using System;
using Roomie.Common.HomeAutomation;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    public class ZWaveDimmerSwitch : IDimmerSwitch
    {
        private ZWaveDevice _device;

        public ZWaveDimmerSwitch(ZWaveDevice device)
        {
            _device = device;
        }

        public int? Power
        {
            get
            {
                return _device.CachedPower;
            }
        }

        public int? MaxPower
        {
            get
            {
                //TODO: store this here
                return _device.MaxPower;
            }
        }

        public void Poll()
        {
            Action operation = () =>
            {
                _device.CachedPower = _device.BackingObject.Level;
            };

            _device.DoDeviceOperation(operation);
        }

        public void SetPower(int power)
        {
            power = Utilities.ValidatePower(power, MaxPower);

            Action operation = () =>
            {
                _device.BackingObject.Level = (byte)power;
            };

            _device.DoDeviceOperation(operation);
        }
    }
}
