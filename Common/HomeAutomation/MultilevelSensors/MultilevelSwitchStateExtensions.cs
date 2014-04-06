using System.Text;
using System.Xml.Linq;
using Roomie.Common.Measurements;

namespace Roomie.Common.HomeAutomation.MultilevelSensors
{
    public static class MultilevelSensorStateExtensions
    {
        public static ReadOnlyMultilevelSensorState<TMeasurement> Copy<TMeasurement>(
            this IMultilevelSensorState<TMeasurement> state)
            where TMeasurement : IMeasurement
        {
            return ReadOnlyMultilevelSensorState<TMeasurement>.CopyFrom(state);
        }

        public static string Describe<TMeasurement>(this IMultilevelSensorState<TMeasurement> state)
            where TMeasurement : IMeasurement
        {
            var result = new StringBuilder();

            if (state == null)
            {
                return result.ToString();
            }

            var value = state.Value;
            if (value != null)
            {
                result.Append(value);
            }

            return result.ToString();
        }

        public static XElement ToXElement<TMeasurement>(this IMultilevelSensorState<TMeasurement> state, string nodeName = "MultilevelSensor")
            where TMeasurement : IMeasurement
        {
            var result = new XElement(nodeName);

            if (state.Value != null)
            {
                result.Add(new XAttribute("Value", state.Value));
            }

            return result;
        }

        public static ReadOnlyMultilevelSensorState<TMeasurement> ToMultilevelSensor<TMeasurement>(this XElement element)
            where TMeasurement : IMeasurement
        {
            return ReadOnlyMultilevelSensorState<TMeasurement>.FromXElement(element);
        }
    }
}
