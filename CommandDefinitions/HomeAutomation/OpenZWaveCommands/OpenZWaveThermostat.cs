using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Cores;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWaveThermostat : IThermostat
    {
        private readonly OpenZWaveThermostatCore _core;
        private readonly OpenZWaveThermostatFan _fan;
        private readonly OpenZWaveSetpointCollection _setpoints;

        public OpenZWaveThermostat(OpenZWaveDevice device)
        {
            _core = new OpenZWaveThermostatCore(device);
            _fan = new OpenZWaveThermostatFan(device);
            _setpoints = new OpenZWaveSetpointCollection(device);
        }

        public bool ProcessValueUpdate(OpenZWaveDeviceValue value, ValueUpdateType updateType)
        {
            if (_core.ProcessValueUpdate(value, updateType))
            {
                return true;
            }

            if (_fan.ProcessValueUpdate(value, updateType))
            {
                return true;
            }

            if (_setpoints.ProcessValueUpdate(value, updateType))
            {
                return true;
            }

            return false;
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
