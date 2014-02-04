using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.BinarySensors;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWaveBinarySensor : IBinarySensor
    {
        public BinarySensorType? Type { get; set; }

        public bool? Value
        {
            get
            {
                if (!_device.Type.Equals(DeviceType.BinarySensor))
                {
                    return null;
                }

                var value = _device.Event;

                if (value == null)
                {
                    return null;
                }

                var result = value > 0;

                return result;
            }
        }

        private readonly OpenZWaveDevice _device;

        public OpenZWaveBinarySensor(OpenZWaveDevice device)
        {
            _device = device;
        }

        public void Poll()
        {
        }
    }
}
