using OpenZWaveDotNet;
using Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.DimmerSwitches;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWaveDimmerSwitch : IDimmerSwitch
    {
        private int? _savedPower;

        private SwitchMultilevelDataEntry _dataEntry;

        public OpenZWaveDimmerSwitch(OpenZWaveDevice device)
        {
            _dataEntry = new SwitchMultilevelDataEntry(device);
            MaxPower = 99;
        }

        internal bool ProcessValueChanged(ZWValueID entry)
        {
            var result = _dataEntry.ProcessValueChanged(entry);

            return result;
        }

        #region IDimmerSwitch

        public int? Power
        {
            get
            {
                var result = (int?)_dataEntry.GetValue() ?? _savedPower;

                return result;
            }
        }

        public int? MaxPower { get; private set; }

        public void Update(IDimmerSwitchState state)
        {
            _savedPower = state.Power;
            MaxPower = state.MaxPower;
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
