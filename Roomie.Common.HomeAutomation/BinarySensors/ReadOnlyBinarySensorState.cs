using System;
using System.Xml.Linq;

namespace Roomie.Common.HomeAutomation.BinarySensors
{
    public class ReadOnlyBinarySensorState : IBinarySensorState
    {
        public BinarySensorType? Type { get; private set; }
        public bool? Value { get; private set; }
        public DateTime? TimeStamp { get; private set; }

        private ReadOnlyBinarySensorState()
        {
        }

        public ReadOnlyBinarySensorState(BinarySensorType type, bool value, DateTime? timeStamp)
        {
            Type = type;
            Value = value;
            TimeStamp = timeStamp;
        }

        public static ReadOnlyBinarySensorState Blank()
        {
            var result = new ReadOnlyBinarySensorState();

            return result;
        }

        public static ReadOnlyBinarySensorState CopyFrom(IBinarySensorState source)
        {
            var result = new ReadOnlyBinarySensorState
            {
                Type = source.Type,
                Value = source.Value,
                TimeStamp = source.TimeStamp
            };

            return result;
        }

        public static ReadOnlyBinarySensorState FromXElement(XElement element)
        {
            var type = element.GetAttributeStringValue("Type").ToBinarySensorTypeNullable();
            var value = element.GetAttributeBoolValue("Value");
            var timeStamp = element.GetAttributeDateTimeValue("TimeStamp");

            var result = new ReadOnlyBinarySensorState
            {
                Type = type,
                Value = value,
                TimeStamp = timeStamp
            };

            return result;
        }
    }
}
