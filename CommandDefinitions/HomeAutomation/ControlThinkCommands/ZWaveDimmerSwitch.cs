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
            set
            {
                value = Utilities.ValidatePower(value, MaxPower);

                Action operation = () =>
                {
                    _device.BackingObject.Level = (byte)value;
                };

                _device.DoDeviceOperation(operation);
            }
        }

        public int MaxPower
        {
            get
            {
                //TODO: store this here
                return _device.MaxPower;
            }
        }

        public int? Percentage
        {
            get
            {
                return Utilities.CalculatePowerPercentage(Power, MaxPower);
            }
        }
    }
}
