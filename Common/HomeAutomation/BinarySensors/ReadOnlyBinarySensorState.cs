using System.Xml.Linq;

namespace Roomie.Common.HomeAutomation.BinarySensors
{
    public class ReadOnlyBinarySensorState : IBinarySensorState
    {
        public BinarySensorType? Type { get; private set; }
        public bool? Value { get; private set; }

        private ReadOnlyBinarySensorState()
        {
        }

        public ReadOnlyBinarySensorState(BinarySensorType type, bool value)
        {
            Type = type;
            Value = value;
        }

        public static ReadOnlyBinarySensorState CopyFrom(IBinarySensorState source)
        {
            var result = new ReadOnlyBinarySensorState
            {
                Type = source.Type,
                Value = source.Value
            };

            return result;
        }

        public static ReadOnlyBinarySensorState FromXElement(XElement element)
        {
            var type = element.GetAttributeStringValue("Type").ToBinarySensorType();
            var value = element.GetAttributeBoolValue("Value");

            var result = new ReadOnlyBinarySensorState
            {
                Type = type,
                Value = value
            };

            return result;
        }
    }
}
