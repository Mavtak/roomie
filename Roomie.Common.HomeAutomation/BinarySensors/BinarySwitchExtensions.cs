using System;
using System.Text;
using System.Xml.Linq;

namespace Roomie.Common.HomeAutomation.BinarySensors
{
    public static class BinarySensorExtensions
    {
        public static IBinarySensorState Copy(this IBinarySensorState state)
        {
            return ReadOnlyBinarySensorState.CopyFrom(state);
        }

        public static string Describe(this IBinarySensorState state, TimeZoneInfo timeZone = null)
        {
            var result = new StringBuilder();

            if (state == null)
            {
                return result.ToString();
            }

            if (state.Value == null)
            {
                result.Append("Unknown");
            }
            else
            {
                var value = state.Value.Value;

                switch (state.Type)
                {
                    case BinarySensorType.Motion:
                        result.Append(value ? "Motion" : "Stillness");
                        break;

                    case BinarySensorType.Door:
                    case BinarySensorType.Window:
                        result.Append(value ? "Open" : "Closed");
                        break;

                    default:
                        result.Append(value);
                        break;
                }
            }

            var timestamp = state.TimeStamp;
            if (timestamp != null)
            {
                result.Append(" (at ");
                result.AppendDateTime(state.TimeStamp, timeZone);
                result.Append(")");
            }

            return result.ToString();
        }

        public static XElement ToXElement(this IBinarySensorState state, string nodeName = "BinarySensor")
        {
            var result = new XElement(nodeName);

            if (state.Type != null)
            {
                result.Add(new XAttribute("Type", state.Type));
            }

            if (state.Value != null)
            {
                result.Add(new XAttribute("Value", state.Value));
            }

            if (state.TimeStamp != null)
            {
                result.Add(new XAttribute("TimeStamp", state.TimeStamp));
            }

            return result;
        }

        public static IBinarySensorState ToBinarySensor(this XElement element)
        {
            return ReadOnlyBinarySensorState.FromXElement(element);
        }
    }
}
