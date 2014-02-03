using System;
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
        public ThermostatCurrentAction? CurrentAction { get; private set; }

        private readonly ThermostatModeDataEntry _mode;

        public OpenZWaveThermostatCore(OpenZWaveDevice device)
        {
            _mode = new ThermostatModeDataEntry(device);
        }

        public bool ProcessValueChanged(OpenZWaveDeviceValue entry)
        {
            if (_mode.ProcessValueChanged(entry))
            {
                return true;
            }

            //TODO: current action

            return false;
        }

        public void PollCurrentAction()
        {
            //TODO: implement
            throw new NotImplementedException();
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
