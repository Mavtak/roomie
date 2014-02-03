using System;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.DimmerSwitches;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    internal class ZWaveDimmerSwitch : IMultilevelSwitch
    {
        private readonly ZWaveDevice _device;

        public ZWaveDimmerSwitch(ZWaveDevice device)
        {
            _device = device;
        }

        public int? Power { get; set; }
        public int? MaxPower { get; set; }

        public void Poll()
        {
            Power = _device.DoDeviceOperation(() => _device.BackingObject.Level);
        }

        public void SetPower(int power)
        {
            power = Utilities.ValidatePower(power, MaxPower);

            _device.DoDeviceOperation(() => _device.BackingObject.Level = (byte)power);

            PullWhileChanging((byte)power);
        }

        public void Update(IMultilevelSwitchState state)
        {
            Power = state.Power;
            MaxPower = state.MaxPower;
        }

        private void PullWhileChanging(byte expectedPower, int tries = 25)
        {
            var lastPower = Power;

            for (var i = 0; i < tries; i++)
            {
                Poll();
                
                if (Power == null || _device.IsConnected == false || Power == expectedPower || lastPower == Power)
                {
                    return;
                }

                lastPower = Power;
            }
        }
    }
}
