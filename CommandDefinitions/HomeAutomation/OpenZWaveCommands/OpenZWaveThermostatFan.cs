using System;
using System.Collections.Generic;
using Roomie.Common.HomeAutomation.Thermostats.Fans;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWaveThermostatFan : IThermostatFan
    {
        public IEnumerable<ThermostatFanMode> SupportedModes { get; private set; }
        public ThermostatFanMode? Mode { get; private set; }
        public ThermostatFanCurrentAction? CurrentAction { get; private set; }

        public OpenZWaveThermostatFan(OpenZWaveDevice device)
        {
        }

        public bool ProcessValueChanged(OpenZWaveDeviceValue entry)
        {
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

        public void SetMode(ThermostatFanMode fanMode)
        {
            //TODO: implement
            throw new NotImplementedException();
        }
    }
}
