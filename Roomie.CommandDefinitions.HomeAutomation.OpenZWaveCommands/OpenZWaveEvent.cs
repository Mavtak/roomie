using System;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.Events;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWaveEvent
    {
        public byte? Value { get; private set; }
        public DateTime? TimeStamp { get; private set; }

        public bool HasValue
        {
            get
            {
                return Value.HasValue;
            }
        }

        private OpenZWaveDevice _device;

        public OpenZWaveEvent(OpenZWaveDevice device)
        {
            _device = device;
        }

        internal void Update(byte? value)
        {
            Value = value;
            TimeStamp = DateTime.UtcNow;

            //TODO: what else is this value used for?
            if (_device.Type.Equals(DeviceType.BinarySensor))
            {
                _device.AddEvent(DeviceEvent.BinarySensorValueChanged(_device, null));
            }
        }
    }
}
