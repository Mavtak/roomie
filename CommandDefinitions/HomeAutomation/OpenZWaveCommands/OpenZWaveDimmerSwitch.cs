﻿using Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.MultilevelSwitches;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWaveDimmerSwitch : IMultilevelSwitch
    {
        private SwitchMultilevelDataEntry _dataEntry;

        public OpenZWaveDimmerSwitch(OpenZWaveDevice device)
        {
            _dataEntry = new SwitchMultilevelDataEntry(device);

            //TODO: only set max power if actually a dimmer switch
            MaxPower = 99;
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

        public int? MaxPower { get; private set; }

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
