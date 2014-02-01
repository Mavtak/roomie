using System;
using System.Collections.Generic;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Measurements.Temperature;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWaveSetpointCollection : IThermostatSetpointCollection
    {
        private readonly Dictionary<ThermostatSetpointType, ITemperature> _setpoints;
        public ITemperature this[ThermostatSetpointType setpointType]
        {
            get
            {
                return _setpoints[setpointType];
            }
        }

        public IEnumerable<ThermostatSetpointType> AvailableSetpoints
        {
            get
            {
                return _setpoints.Keys;
            }
        }

        public OpenZWaveSetpointCollection(OpenZWaveDevice device)
        {
            _setpoints = new Dictionary<ThermostatSetpointType, ITemperature>();
        }

        public bool ProcessValueChanged(OpenZWaveDeviceValue entry)
        {
            //TODO: implement
            return false;
        }

        public void PollSupportedSetpoints()
        {
            //TODO: implement
            throw new NotImplementedException();
        }

        public void PollSetpointTemperatures()
        {
            //TODO: implement
            throw new NotImplementedException();
        }

        public void SetSetpoint(ThermostatSetpointType setpointType, ITemperature temperature)
        {
            throw new NotImplementedException();
        }
    }
}
