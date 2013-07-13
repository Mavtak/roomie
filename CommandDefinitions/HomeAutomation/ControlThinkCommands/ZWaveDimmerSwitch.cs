using System;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.DimmerSwitches;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    public class ZWaveDimmerSwitch : IDimmerSwitch
    {
        private ZWaveDevice _device;

        public ZWaveDimmerSwitch(ZWaveDevice device)
        {
            _device = device;
        }

        public int? Power { get; set; }
        public int? MaxPower { get; set; }

        public void Poll()
        {
            Action operation = () =>
            {
                Power = _device.BackingObject.Level;
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

        public void Update(IDimmerSwitchState state)
        {
            Power = state.Power;
            MaxPower = state.MaxPower;
        }
    }
}
