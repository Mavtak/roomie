using System;
using Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific;
using Roomie.Common.HomeAutomation.MultilevelSensors;
using Roomie.Common.Measurements.Illuminance;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWaveIlluminanceSensor : IMultilevelSensor<IIlluminance>
    {
        private readonly IlluminanceSensorDataEntry _dataEntry;

        public OpenZWaveIlluminanceSensor(OpenZWaveDevice device)
        {
            _dataEntry = new IlluminanceSensorDataEntry(device);
        }

        internal bool ProcessValueUpdate(OpenZWaveDeviceValue value, ValueUpdateType updateType)
        {
            var result = _dataEntry.ProcessValueUpdate(value, updateType);

            return result;
        }

        #region IHumiditySensor

        public IIlluminance Value
        {
            get
            {
                var result = _dataEntry.GetValue();

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

        public void Poll()
        {
            _dataEntry.RefreshValue();
        }

        #endregion
    }
}
