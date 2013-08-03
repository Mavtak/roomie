using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.DimmerSwitches;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    internal class ZWaveDimmerSwitch : IDimmerSwitch
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
            _device.DoDeviceOperation(() => Power = _device.BackingObject.Level);
        }

        public void SetPower(int power)
        {
            power = Utilities.ValidatePower(power, MaxPower);

            _device.DoDeviceOperation(() => _device.BackingObject.Level = (byte)power);
        }

        public void Update(IDimmerSwitchState state)
        {
            Power = state.Power;
            MaxPower = state.MaxPower;
        }
    }
}
