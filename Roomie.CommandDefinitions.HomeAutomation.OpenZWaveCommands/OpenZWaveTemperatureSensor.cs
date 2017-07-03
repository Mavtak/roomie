using System;
using Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific;
using Roomie.Common.HomeAutomation.MultilevelSensors;
using Roomie.Common.Measurements.Temperature;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWaveTemperatureSensor : IMultilevelSensor<ITemperature>
    {
        private readonly ThermometerDataEntry _dataEntry;

        public OpenZWaveTemperatureSensor(OpenZWaveDevice device)
        {
            _dataEntry = new ThermometerDataEntry(device);
        }

        internal bool ProcessValueUpdate(OpenZWaveDeviceValue value, ValueUpdateType updateType)
        {
            var result = _dataEntry.ProcessValueUpdate(value, updateType);

            return result;
        }

        #region ITemperatureSensor

        public ITemperature Value
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
