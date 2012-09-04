using System;
using System.Xml.Linq;

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
    }
}
