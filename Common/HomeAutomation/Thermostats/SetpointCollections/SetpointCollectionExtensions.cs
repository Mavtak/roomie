
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Roomie.Common.Temperature;

namespace Roomie.Common.HomeAutomation.Thermostats.SetpointCollections
{
    public static class SetpointCollectionExtensions
    {
        public static ReadOnlySetpointCollection Copy(this ISetpointCollectionState source)
        {
            return ReadOnlySetpointCollection.CopyFrom(source);
        }

        public static string Describe(this ISetpointCollectionState state)
        {
            var result = new StringBuilder();
            
            if (state == null)
            {
                return result.ToString();
            }

            var setpointTypes = state.AvailableSetpoints.ToList();

            if (setpointTypes.Count == 0)
            {
                return result.ToString();
            }

            foreach (var setpointType in setpointTypes)
            {
                if (result.Length > 0)
                {
                    result.Append(", ");
                }

                result.Append(setpointType);

                var temperature = state[setpointType];

                result.Append(" at ");
                
                if (temperature == null)
                {
                    result.Append("?");
                }
                else
                {
                    result.Append(temperature);
                }
            }

            return result.ToString();
        }

        public static Dictionary<SetpointType, ITemperature> ToDictionary(this ISetpointCollectionState state)
        {
            if (state == null)
            {
                return null;
            }

            var availableSetpoints = state.AvailableSetpoints;

            if (availableSetpoints == null)
            {
                return null;
            }

            var result = availableSetpoints.ToDictionary(x => x, type => state[type]);

            return result;
        }

        public static XElement ToXElement(this ISetpointCollectionState state, string nodeName = "Setpoints")
        {
            var result = new XElement(nodeName);

            if (state != null)
            {
                var setpoints = state.ToDictionary();

                if (setpoints != null && setpoints.Count > 0)
                {
                    foreach (var setpoint in setpoints.Keys)
                    {
                        var temperature = setpoints[setpoint];
                        result.Add(new XElement(setpoint.ToString(), temperature));
                    }
                }
            }

            return result;
        }

        public static ReadOnlySetpointCollection ToSetpoints(this XElement element)
        {
            return ReadOnlySetpointCollection.FromXElement(element);
        }
    }
}
