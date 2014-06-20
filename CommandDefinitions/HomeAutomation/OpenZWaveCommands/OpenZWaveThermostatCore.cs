using System.Collections.Generic;
using System.Linq;
using Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Cores;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWaveThermostatCore : IThermostatCore
    {
        public IEnumerable<ThermostatMode> SupportedModes
        {
            get
            {
                var result = _mode.GetOptions().Where(x => x.HasValue).Select(x => x.Value).ToArray();

                return result;
            }
        }
        public ThermostatMode? Mode
        {
            get
            {
                var result = _mode.GetValue();

                return result;
            }
        }
        public ThermostatCurrentAction? CurrentAction
        {
            get
            {
                var result = _currentAction.GetValue();

                return result;
            }
        }

        private readonly ThermostatModeDataEntry _mode;
        private readonly ThermostatCoreCurrentActionDataEntry _currentAction;

        public OpenZWaveThermostatCore(OpenZWaveDevice device)
        {
            _mode = new ThermostatModeDataEntry(device);
            _currentAction = new ThermostatCoreCurrentActionDataEntry(device);
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

        public void SetMode(ThermostatMode mode)
        {
            //TODO: what happens for unsupported values?
            _mode.SetValue(mode);
        }
    }
}
