using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Update(IThermostatSetpointCollectionState state)
        {
            var originalKeys = _setpoints.Keys.ToList();

            foreach (var key in state.AvailableSetpoints)
            {
                var value = state[key];

                if (originalKeys.Contains(key))
                {
                    _setpoints[key] = value;
                    originalKeys.Remove(key);
                }
                else
                {
                    Add(key, value);
                }
            }

            foreach (var key in originalKeys)
            {
                _setpoints.Remove(key);
            }
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
            _device.DoCommand("SetSetpoint", "Setpoint", setpointType.ToString(), "Temperature", temperature.ToString());
        }

        public void Add(ThermostatSetpointType setpoint, ITemperature temperature)
        {
            _setpoints.Add(setpoint, temperature);
        }
    }
}
