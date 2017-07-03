using System.Collections.Generic;
using System.Linq;
using Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific;
using Roomie.Common.HomeAutomation.Thermostats.Fans;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWaveThermostatFan : IThermostatFan
    {
        public IEnumerable<ThermostatFanMode> SupportedModes
        {
            get
            {
                var result = _mode.GetOptions().Where(x => x.HasValue).Select(x => x.Value).ToArray();

                return result;
            }
        }

        public ThermostatFanMode? Mode
        {
            get
            {
                var result = _mode.GetValue();

                return result;
            }
        }

        public ThermostatFanCurrentAction? CurrentAction
        {
            get
            {
                var result = _currentAction.GetValue();

                return result;
            }
        }

        private readonly ThermostatFanModeDataEntry _mode;
        private readonly ThermostatFanCurrentActionDataEntry _currentAction;


        public OpenZWaveThermostatFan(OpenZWaveDevice device)
        {
            _mode = new ThermostatFanModeDataEntry(device);
            _currentAction = new ThermostatFanCurrentActionDataEntry(device);
        }

        public bool ProcessValueUpdate(OpenZWaveDeviceValue value, ValueUpdateType updateType)
        {
            if (_mode.ProcessValueUpdate(value, updateType))
            {
                return true;
            }

            if (_currentAction.ProcessValueUpdate(value, updateType))
            {
                return true;
            }

            return false;
        }

        public void PollCurrentAction()
        {
            _currentAction.RefreshValue();
        }

        public void PollMode()
        {
            _mode.RefreshValue();
        }

        public void PollSupportedModes()
        {
            //TODO: rethink this interface.
            PollMode();
        }

        public void SetMode(ThermostatFanMode mode)
        {
            //TODO: what happens for unsupported values?
            _mode.SetValue(mode);
        }
    }
}
