using System;
using System.Collections.Generic;
using System.Linq;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Measurements.Temperature;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Persistence.Models
{
    public class ThermostatSetpointModel : Dictionary<ThermostatSetpointType, ITemperature>, IThermostatSetpointCollection
    {
        private Device _device;

        public IEnumerable<ThermostatSetpointType> AvailableSetpoints
        {
            get
            {
                return Keys;
            }
        }

        public ThermostatSetpointModel(Device device)
        {
            _device = device;
        }

        public void Update(IThermostatSetpointCollectionState state)
        {
            var originalKeys = Keys.ToList();

            foreach (var key in state.AvailableSetpoints)
            {
                var value = state[key];

                if (originalKeys.Contains(key))
                {
                    this[key] = value;
                    originalKeys.Remove(key);
                }
                else
                {
                    Add(key, value);
                }
            }

            foreach (var key in originalKeys)
            {
                Remove(key);
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
    }
}
