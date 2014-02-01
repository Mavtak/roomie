using System;
using System.Collections.Generic;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Cores;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWaveThermostatCore : IThermostatCore
    {
        public IEnumerable<ThermostatMode> SupportedModes { get; private set; }
        public ThermostatMode? Mode { get; private set; }
        public ThermostatCurrentAction? CurrentAction { get; private set; }

        public OpenZWaveThermostatCore(OpenZWaveDevice device)
        {
        }

        public bool ProcessValueChanged(OpenZWaveDeviceValue entry)
        {
            //TODO: implement
            return false;
        }

        public void PollCurrentAction()
        {
            //TODO: implement
            throw new NotImplementedException();
        }

        public void PollMode()
        {
            //TODO: implement
            throw new NotImplementedException();
        }

        public void PollSupportedModes()
        {
            //TODO: implement
            throw new NotImplementedException();
        }

        public void SetMode(ThermostatMode mode)
        {
            //TODO: implement
            throw new NotImplementedException();
        }
    }
}
