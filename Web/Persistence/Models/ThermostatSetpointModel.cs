using System;
using System.Collections.Generic;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Measurements.Temperature;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Persistence.Models
{
    public class ThermostatSetpointModel : IThermostatSetpointCollection
    {
        private Dictionary<ThermostatSetpointType, ITemperature> _setpoints;
 
        private DeviceModel _device;

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

        public ThermostatSetpointModel(DeviceModel device)
        {
            _device = device;
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
            _device.DoCommand("HomeAutomation.SetSetpoint Device=\"{0}\" Setpoint=\"{1}\" Temperature=\"{2}\"", setpointType.ToString(), temperature.ToString());
        }

        public void Add(ThermostatSetpointType setpoint, ITemperature temperature)
        {
            _setpoints.Add(setpoint, temperature);
        }
    }
}
