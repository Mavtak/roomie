using Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific;
using Roomie.Common.HomeAutomation.MultilevelSensors;
using Roomie.Common.Measurements.Power;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWavePowerSensor : IMultilevelSensor<IPower>
    {
        private readonly ImmediatePowerDataEntry _dataEntry;

        private readonly OpenZWaveDevice _device;

        public OpenZWavePowerSensor(OpenZWaveDevice device)
        {
            _device = device;
            _dataEntry = new ImmediatePowerDataEntry(device);
        }

        internal bool ProcessValueChanged(OpenZWaveDeviceValue entry)
        {
            var result = _dataEntry.ProcessValueChanged(entry);

            return result;
        }

        #region IPowerSensor

        public IPower Value
        {
            get
            {
                var value = _dataEntry.GetValue() ?? -1;

                if (value < 0)
                {
                    return null;
                }

                //TODO: read and parse units

                var result = new WattsPower((double) value);

                return result;
            }
        }

        public void Poll()
        {
            _dataEntry.RefreshValue();
        }

        #endregion
    }
}
