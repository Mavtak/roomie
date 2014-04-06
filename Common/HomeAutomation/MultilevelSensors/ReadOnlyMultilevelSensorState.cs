using System;
using System.Xml.Linq;
using Roomie.Common.Measurements;

namespace Roomie.Common.HomeAutomation.MultilevelSensors
{
    public class ReadOnlyMultilevelSensorState<TMeasurement> : IMultilevelSensorState<TMeasurement>
        where TMeasurement : IMeasurement
    {
        public TMeasurement Value { get; private set; }

        private ReadOnlyMultilevelSensorState()
        {
        }

        public ReadOnlyMultilevelSensorState(TMeasurement value)
        {
            Value = value;
        }

        public static ReadOnlyMultilevelSensorState<TMeasurement> CopyFrom(IMultilevelSensorState<TMeasurement> source)
        {
            var result = new ReadOnlyMultilevelSensorState<TMeasurement>
                {
                    Value = source.Value
                };

            return result;
        }

        public static ReadOnlyMultilevelSensorState<TMeasurement> FromXElement(XElement element)
        {
            var value = default(TMeasurement);

            var valueString = element.GetAttributeStringValue("Value");
            if (valueString != null)
            {
                value = MeasurementParser.Parse<TMeasurement>(valueString);
            }

            var result = new ReadOnlyMultilevelSensorState<TMeasurement>
                {
                    Value = value
                };

            return result;
        }
    }
}
