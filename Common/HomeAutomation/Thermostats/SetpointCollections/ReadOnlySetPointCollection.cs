using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Roomie.Common.Temperature;

namespace Roomie.Common.HomeAutomation.Thermostats.SetpointCollections
{
    public class ReadOnlySetpointCollection : ISetpointCollectionState
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

        public ReadOnlySetpointCollection()
        {
            _setpoints = new Dictionary<SetpointType, ITemperature>();
        }

        public ReadOnlySetpointCollection(Dictionary<SetpointType, ITemperature> setpoints)
        {
            _setpoints = setpoints;
        }

        public static ReadOnlySetpointCollection CopyFrom(ISetpointCollectionState source)
        {
            var result = new ReadOnlySetpointCollection
            {
                _setpoints = source.AvailableSetpoints.ToDictionary(setpoint => setpoint, setpoint => source[setpoint])
            };

            return result;
        }

        public static ReadOnlySetpointCollection Empty()
        {
            var result = new ReadOnlySetpointCollection
                {
                    _setpoints = new Dictionary<SetpointType, ITemperature>()
                };

            return result;
        }

        public static ReadOnlySetpointCollection FromXElement(XElement element)
        {
            var setpoints = new Dictionary<SetpointType, ITemperature>();

            foreach (var setpointElement in element.Elements())
            {
                var setpointType = setpointElement.Name.LocalName.ToSetpointType();
                var temperature = (string.IsNullOrEmpty(setpointElement.Value)) ? null : setpointElement.Value.ToTemperature();

                setpoints.Add(setpointType, temperature);
            }

            var result = new ReadOnlySetpointCollection
                {
                    _setpoints = setpoints
                };

            return result;
        }
    }
}
