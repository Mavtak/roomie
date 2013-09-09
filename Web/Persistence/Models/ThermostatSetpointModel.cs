using System;
using System.Collections.Generic;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Temperature;

namespace Roomie.Web.Persistence.Models
{
    public class ThermostatSetpointModel : IThermostatSetpointCollection
    {
        private Dictionary<ThermostatSetpointType, ITemperature> _setpoints;
 
        public ITemperature this[ThermostatSetpointType setpoint]
        {
            get
            {
                return _setpoints[setpoint];
            }
        }

        public IEnumerable<ThermostatSetpointType> AvailableSetpoints
        {
            get
            {
                return _setpoints.Keys;
            }
        }

        public ThermostatSetpointModel()
        {
            _setpoints = new Dictionary<ThermostatSetpointType, ITemperature>();
        }

        public void PollSupportedSetpoints()
        {
            throw new NotImplementedException();
        }

        public void PollSetpointTemperatures()
        {
            throw new NotImplementedException();
        }

        public void SetSetpoint(ThermostatSetpointType setpointType, ITemperature temperature)
        {
            throw new NotImplementedException();
        }

        public void Add(ThermostatSetpointType setpoint, ITemperature temperature)
        {
            _setpoints.Add(setpoint, temperature);
        }
    }
}
