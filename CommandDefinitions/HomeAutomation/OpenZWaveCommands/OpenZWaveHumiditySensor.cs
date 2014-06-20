using System;
using Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific;
using Roomie.Common.HomeAutomation.MultilevelSensors;
using Roomie.Common.Measurements.Humidity;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWaveHumiditySensor : IMultilevelSensor<IHumidity>
    {
        private readonly HumiditySensorDataEntry _dataEntry;

        public OpenZWaveHumiditySensor(OpenZWaveDevice device)
        {
            _dataEntry = new HumiditySensorDataEntry(device);
        }

        internal bool ProcessValueUpdate(OpenZWaveDeviceValue value, ValueUpdateType updateType)
        {
            var result = _dataEntry.ProcessValueUpdate(value, updateType);

            return result;
        }

        #region IHumiditySensor

        public IHumidity Value
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
