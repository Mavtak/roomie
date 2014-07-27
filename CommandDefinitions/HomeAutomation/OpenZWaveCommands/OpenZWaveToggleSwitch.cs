using Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific;
using Roomie.Common.HomeAutomation.BinarySwitches;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWaveToggleSwitch : IBinarySwitch
    {
        private readonly SwitchBinaryDataEntry _dataEntry;
        private readonly OpenZWaveDevice _device;

        public OpenZWaveToggleSwitch(OpenZWaveDevice device)
        {
            _device = device;
            _dataEntry = new SwitchBinaryDataEntry(device);
        }

        internal bool ProcessValueUpdate(OpenZWaveDeviceValue value, ValueUpdateType updateType)
        {
            var result = _dataEntry.ProcessValueUpdate(value, updateType);

            if (result)
            {
                _device.UpdateCurrentAction();
            }

            return result;
        }

        #region IToggleSwitch

        public BinarySwitchPower? Power
        {
            get
            {
                var boolValue = _dataEntry.GetValue();
                var result = BoolToToggleSwitchPower(boolValue);

                return result;
            }
        }

        public void SetPower(BinarySwitchPower power)
        {
            switch (power)
            {
                case BinarySwitchPower.On:
                    _dataEntry.SetValue(true);
                    break;

                case BinarySwitchPower.Off:
                    _dataEntry.SetValue(false);
                    break;
            }
        }

        public void Poll()
        {
            _dataEntry.RefreshValue();
        }

        #endregion

        private static BinarySwitchPower? BoolToToggleSwitchPower(bool? input)
        {
            BinarySwitchPower? result;

            switch (input)
            {
                case true:
                    result = BinarySwitchPower.On;
                    break;

                case false:
                    result = BinarySwitchPower.Off;
                    break;

                default:
                    result = null;
                    break;
            }

            return result;
        }
    }
}
