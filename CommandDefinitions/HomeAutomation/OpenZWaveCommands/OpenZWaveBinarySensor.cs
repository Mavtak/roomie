using System;
using Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.BinarySensors;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWaveBinarySensor : IBinarySensor
    {
        public BinarySensorType? Type { get; set; }

        private readonly BinarySensorDataEntry _dataEntry;

        public bool? Value
        {
            get
            {
                var result = GetValueFromDataEntry() ?? GetValueFromEventValue();

                return result;
            }
        }

        public DateTime? TimeStamp
        {
            get
            {
                return _dataEntry.LastUpdated;
            }
        }

        private readonly OpenZWaveDevice _device;

        public OpenZWaveBinarySensor(OpenZWaveDevice device)
        {
            _device = device;
            _dataEntry = new BinarySensorDataEntry(device);
        }

        private bool? GetValueFromDataEntry()
        {
            var result = _dataEntry.GetValue();

            return result;
        }

        private bool? GetValueFromEventValue()
        {
            if (!_device.Type.Equals(DeviceType.BinarySensor))
            {
                return null;
            }

            var value = _device.Event.Value;

            if (value == null)
            {
                return null;
            }

            var result = value > 0;

            return result;
        }

        public void Poll()
        {
        }

        internal bool ProcessValueChanged(OpenZWaveDeviceValue entry)
        {
            var result = _dataEntry.ProcessValueChanged(entry);

            return result;
        }
    }
}
