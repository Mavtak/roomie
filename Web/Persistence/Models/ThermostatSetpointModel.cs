using System;
using System.Collections.Generic;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Temperature;

namespace Roomie.Web.Persistence.Models
{
    public class ThermostatSetpointModel : ISetpointCollection
    {
        private Dictionary<SetpointType, ITemperature> _setpoints;
 
        public ITemperature this[SetpointType setpoint]
        {
            get
            {
                return _setpoints[setpoint];
            }
        }

        public IEnumerable<SetpointType> AvailableSetpoints
        {
            get
            {
                return _setpoints.Keys;
            }
        }

        public ThermostatSetpointModel()
        {
            _setpoints = new Dictionary<SetpointType, ITemperature>();
        }

        public void PollSupportedSetpoints()
        {
            throw new NotImplementedException();
        }

        public void PollSetpointTemperatures()
        {
            throw new NotImplementedException();
        }

        public void SetSetpoint(SetpointType setpointType, ITemperature temperature)
        {
            throw new NotImplementedException();
        }

        public void Add(SetpointType setpoint, ITemperature temperature)
        {
            _setpoints.Add(setpoint, temperature);
        }
    }
}
