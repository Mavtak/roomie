using System.Collections.Generic;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.Temperature;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    public class ZWaveSetpointCollection : ISetpointCollection
    {
        private Dictionary<SetpointType, ITemperature> _setpoints; 
        public ITemperature this[SetpointType setpointType]
        {
            get
            {
                return _setpoints[setpointType];
            }
        }

        public IEnumerable<SetpointType> AvailableSetpoints
        {
            get
            {
                return _setpoints.Keys;
            }
        }

        public ZWaveSetpointCollection()
        {
            _setpoints = new Dictionary<SetpointType, ITemperature>();
        }

        internal void Update(SetpointType setpointType, ITemperature temperature)
        {
            if (_setpoints.ContainsKey(setpointType))
            {
                _setpoints[setpointType] = temperature;
            }
            else
            {
                _setpoints.Add(setpointType, temperature);
            }
        }
    }
}
