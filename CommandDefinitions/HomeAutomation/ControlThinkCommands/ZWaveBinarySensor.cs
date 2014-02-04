using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.BinarySensors;
using Roomie.Common.HomeAutomation.BinarySwitches;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    internal class ZWaveBinarySensor : IBinarySensor
    {
        private readonly ZWaveDevice _device;

        public ZWaveBinarySensor(ZWaveDevice device)
        {
            _device = device;
        }

        public BinarySensorType? Type { get; set; }

        public bool? Value
        {
            get
            {
                if (!_device.Type.Equals(DeviceType.BinarySensor))
                {
                    return null;
                }

                var result = _device.BinarySwitch.Power == BinarySwitchPower.On;

                return result;
            }
        }

        public void Poll()
        {
            //TODO: implement this
        }
    }
}
