using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Roomie.Common.Temperature;

namespace Roomie.Common.HomeAutomation.Thermostats.SetpointCollections
{
    public class ReadOnlyThermostatSetpointCollection : IThermostatSetpointCollectionState
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

        private ReadOnlyThermostatSetpointCollection()
        {
            _setpoints = new Dictionary<ThermostatSetpointType, ITemperature>();
        }

        public ReadOnlyThermostatSetpointCollection(Dictionary<ThermostatSetpointType, ITemperature> setpoints)
        {
            _setpoints = setpoints;
        }

        public static ReadOnlyThermostatSetpointCollection CopyFrom(IThermostatSetpointCollectionState source)
        {
            var result = new ReadOnlyThermostatSetpointCollection
            {
                _setpoints = source.AvailableSetpoints.ToDictionary(setpoint => setpoint, setpoint => source[setpoint])
            };

            return result;
        }

        public static ReadOnlyThermostatSetpointCollection Empty()
        {
            var result = new ReadOnlyThermostatSetpointCollection
                {
                    _setpoints = new Dictionary<ThermostatSetpointType, ITemperature>()
                };

            return result;
        }

        public static ReadOnlyThermostatSetpointCollection FromXElement(XElement element)
        {
            var setpoints = new Dictionary<ThermostatSetpointType, ITemperature>();

            foreach (var setpointElement in element.Elements())
            {
                var setpointType = setpointElement.Name.LocalName.ToSetpointType();
                var temperature = (string.IsNullOrEmpty(setpointElement.Value)) ? null : setpointElement.Value.ToTemperature();

                setpoints.Add(setpointType, temperature);
            }

            var result = new ReadOnlyThermostatSetpointCollection
                {
                    _setpoints = setpoints
                };

            return result;
        }
    }
}
