using System;
using System.Linq;
using System.Xml.Linq;
using Roomie.Common.Measurements;

namespace Roomie.Common
{
    public static class XElementExtensions
    {
        public static string GetAttributeStringValue(this XElement element, string attribute)
        {
            if (element.Attribute(attribute) != null)
            {
                return element.Attribute(attribute).Value;
            }
            else
            {
                return null;
            }
        }

        public static bool? GetAttributeBoolValue(this XElement element, string attribute)
        {
            var value = element.GetAttributeStringValue(attribute);

            if (value == null)
            {
                return null;
            }

            return Convert.ToBoolean(value);
        }

        public static int? GetAttributeIntValue(this XElement element, string attribute)
        {
            var value = element.GetAttributeStringValue(attribute);

            if (value == null)
            {
                return null;
            }

            return Convert.ToInt32(value);
        }

        public static DateTime? GetAttributeDateTimeValue(this XElement element, string attribute)
        {
            var value = element.GetAttributeStringValue(attribute);

            if (value == null)
            {
                return null;
            }

            return Convert.ToDateTime(value).ToUniversalTime();
        }

        public static TMeasurement GetAttributeMeasurementValue<TMeasurement>(this XElement element, string attribute)
            where TMeasurement : IMeasurement
        {
            var valueString = element.GetAttributeStringValue("Value");
            if (valueString == null)
            {
                return default(TMeasurement);
            }

            return MeasurementParser.Parse<TMeasurement>(valueString);
        }

        public static void AddIfHasData(this XElement root, XElement element)
        {
            if (!element.Attributes().Any() && !element.Descendants().Any())
            {
                return;
            }

            root.Add(element);
        }
    }
}
