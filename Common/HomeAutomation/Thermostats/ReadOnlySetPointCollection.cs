using System.Collections.Generic;
using System.Linq;
using Roomie.Common.Temperature;

namespace Roomie.Common.HomeAutomation.Thermostats
{
    public class ReadOnlySetPointCollection : ISetpointCollection
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

        public ReadOnlySetPointCollection()
        {
            _setpoints = new Dictionary<SetpointType, ITemperature>();
        }

        public static ReadOnlySetPointCollection CopyFrom(ISetpointCollection source)
        {
            var result = new ReadOnlySetPointCollection
            {
                _setpoints = source.AvailableSetpoints.ToDictionary(setpoint => setpoint, setpoint => source[setpoint])
            };

            return result;
        }
    }
}
