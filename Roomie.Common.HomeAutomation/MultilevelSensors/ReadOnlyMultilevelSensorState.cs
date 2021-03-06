﻿using System;
using System.Xml.Linq;
using Roomie.Common.Measurements;

namespace Roomie.Common.HomeAutomation.MultilevelSensors
{
    public class ReadOnlyMultilevelSensorState<TMeasurement> : IMultilevelSensorState<TMeasurement>
        where TMeasurement : IMeasurement
    {
        public TMeasurement Value { get; private set; }
        public DateTime? TimeStamp { get; private set; }

        private ReadOnlyMultilevelSensorState()
        {
        }

        public ReadOnlyMultilevelSensorState(TMeasurement value, DateTime? timeStamp)
        {
            Value = value;
            TimeStamp = timeStamp;
        }

        public static ReadOnlyMultilevelSensorState<TMeasurement> Blank()
        {
            var result = new ReadOnlyMultilevelSensorState<TMeasurement>();

            return result;
        }

        public static ReadOnlyMultilevelSensorState<TMeasurement> CopyFrom(IMultilevelSensorState<TMeasurement> source)
        {
            var result = new ReadOnlyMultilevelSensorState<TMeasurement>
                {
                    Value = source.Value,
                    TimeStamp = source.TimeStamp
                };

            return result;
        }

        public static ReadOnlyMultilevelSensorState<TMeasurement> FromXElement(XElement element)
        {
            var value = element.GetAttributeMeasurementValue<TMeasurement>("Value");

            var timeStamp = element.GetAttributeDateTimeValue("TimeStamp");

            var result = new ReadOnlyMultilevelSensorState<TMeasurement>
                {
                    Value = value,
                    TimeStamp = timeStamp
                };

            return result;
        }
    }
}
