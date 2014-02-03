using Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific;
using Roomie.Common.HomeAutomation.ToggleSwitches;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWaveToggleSwitch : IBinarySwitch
    {
        private readonly SwitchBinaryDataEntry _dataEntry;

        public OpenZWaveToggleSwitch(OpenZWaveDevice device)
        {
            _dataEntry = new SwitchBinaryDataEntry(device);
        }

        internal bool ProcessValueChanged(OpenZWaveDeviceValue entry)
        {
            var result = _dataEntry.ProcessValueChanged(entry);

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

        public void PowerOn()
        {
            _dataEntry.SetValue(true);
        }

        public void PowerOff()
        {
            _dataEntry.SetValue(false);
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
