using Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.MultilevelSwitches;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWaveDimmerSwitch : IMultilevelSwitch
    {
        private OpenZWaveDevice _device;
        private SwitchMultilevelDataEntry _dataEntry;

        public OpenZWaveDimmerSwitch(OpenZWaveDevice device)
        {
            _device = device;
            _dataEntry = new SwitchMultilevelDataEntry(device);
        }

        internal bool ProcessValueUpdate(OpenZWaveDeviceValue value, ValueUpdateType updateType)
        {
            var result = _dataEntry.ProcessValueUpdate(value, updateType);

            return result;
        }

        #region IDimmerSwitch

        public int? Power
        {
            get
            {
                var result = (int?)_dataEntry.GetValue();

                return result;
            }
        }

        public int? MaxPower
        {
            get
            {
                if (_device.Type == null || !_device.Type.Equals(DeviceType.MultilevelSensor))
                {
                    return null;
                }

                return 99;
            }
        }

        public void Poll()
        {
            _dataEntry.RefreshValue();
        }

        public void SetPower(int power)
        {
            power = Utilities.ValidatePower(power, MaxPower);

            _dataEntry.SetValue((byte)power);
        }

        #endregion
    }
}
