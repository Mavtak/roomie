using System;
using Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Cores;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Measurements.Temperature;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWaveThermostat : IThermostat
    {
        public ITemperature Temperature
        {
            get
            {
                return _thermometer.GetValue();
            }
        }

        private readonly OpenZWaveThermostatCore _core;
        private readonly OpenZWaveThermostatFan _fan;
        private readonly OpenZWaveSetpointCollection _setpoints;

        private readonly ThermometerDataEntry _thermometer;

        public OpenZWaveThermostat(OpenZWaveDevice device)
        {
            _core = new OpenZWaveThermostatCore(device);
            _fan = new OpenZWaveThermostatFan(device);
            _setpoints = new OpenZWaveSetpointCollection(device);

            _thermometer = new ThermometerDataEntry(device);
        }

        public bool ProcessValueChanged(OpenZWaveDeviceValue entry)
        {
            if (_core.ProcessValueChanged(entry))
            {
                return true;
            }

            if (_fan.ProcessValueChanged(entry))
            {
                return true;
            }

            if (_setpoints.ProcessValueChanged(entry))
            {
                return true;
            }

            if (_thermometer.ProcessValueChanged(entry))
            {
                return true;
            }

            return false;
        }

        public void PollTemperature()
        {
            _thermometer.RefreshValue();
        }

        #region IThermostat definitions

        IThermostatCore IThermostat.Core
        {
            get
            {
                return _core;
            }
        }

        IThermostatFan IThermostat.Fan
        {
            get
            {
                return _fan;
            }
        }

        IThermostatSetpointCollection IThermostat.Setpoints
        {
            get
            {
                return _setpoints;
            }
        }

        #endregion

        #region IThermostatState definitions

        IThermostatCoreState IThermostatState.CoreState
        {
            get
            {
                return _core;
            }
        }

        IThermostatFanState IThermostatState.FanState
        {
            get
            {
                return _fan;
            }
        }

        IThermostatSetpointCollectionState IThermostatState.SetpointStates
        {
            get
            {
                return _setpoints;
            }
        }

        #endregion

        #region IThermostatActions definitions

        IThermostatCoreActions IThermostatActions.CoreActions
        {
            get
            {
                return _core;
            }
        }
        IThermostatFanActions IThermostatActions.FanActions
        {
            get
            {
                return _fan;
            }
        }

        IThermostatSetpointCollectionActions IThermostatActions.SetpointActions
        {
            get
            {
                return _setpoints;
            }
        }

        #endregion
    }
}
