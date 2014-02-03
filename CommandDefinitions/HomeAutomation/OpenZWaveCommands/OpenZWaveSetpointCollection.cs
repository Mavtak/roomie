using System;
using System.Collections.Generic;
using System.Linq;
using Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Measurements.Temperature;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWaveSetpointCollection : IThermostatSetpointCollection
    {
        public ITemperature this[ThermostatSetpointType setpointType]
        {
            get
            {
                ThermostatSetpointDataEntry dataEntry = _setpoints[setpointType];

                //TODO: maybe this could be more relaxed?
                if (!dataEntry.HasValue())
                {
                    throw new IndexOutOfRangeException();
                }

                var result = dataEntry.GetValue();

                return result;
            }
        }

        public IEnumerable<ThermostatSetpointType> AvailableSetpoints
        {
            get
            {
                foreach (var pair in _setpoints)
                {
                    if (pair.Value.HasValue())
                    {
                        yield return pair.Key;
                    }
                }
            }
        }

        private readonly Dictionary<ThermostatSetpointType, ThermostatSetpointDataEntry> _setpoints;

        public OpenZWaveSetpointCollection(OpenZWaveDevice device)
        {
            _setpoints = new Dictionary<ThermostatSetpointType, ThermostatSetpointDataEntry>();

            var setpointTypes = new[]
            {
                ThermostatSetpointType.Heat,
                ThermostatSetpointType.Cool
            };

            foreach (var setpointType in setpointTypes)
            {
                var setpoint = new ThermostatSetpointDataEntry(device, setpointType);

                _setpoints.Add(setpointType, setpoint);
            }
        }

        public bool ProcessValueChanged(OpenZWaveDeviceValue entry)
        {
            var availableSetpoints = _setpoints.Values.Where(x => x.HasValue()).ToArray();

            foreach (var setpoint in availableSetpoints)
            {
                if (setpoint.ProcessValueChanged(entry))
                {
                    return true;
                }
            }

            return false;
        }

        public void PollSupportedSetpoints()
        {
            //TODO: implement
        }

        public void PollSetpointTemperatures()
        {
            var setpointTypesToPoll = AvailableSetpoints.ToArray();
            
            foreach (var setpointType in setpointTypesToPoll)
            {
                var setpoint = _setpoints[setpointType];
                setpoint.RefreshValue();
            }
        }

        public void SetSetpoint(ThermostatSetpointType setpointType, ITemperature temperature)
        {
            var setpoint = _setpoints[setpointType];

            setpoint.SetValue(temperature);
        }
    }
}
