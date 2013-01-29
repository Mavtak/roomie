using System.Text;
using System.Xml;

namespace Roomie.Desktop.Engine
{
    public class RoomieCommandArgument
    {
        public readonly string Name;
        public readonly string Type;
        public readonly string DefaultValue;
        public readonly bool HasDefault;

        public RoomieCommandArgument(string name, string type, string defaultValue = null, bool hasDefault = false)
        {
            Name = name;
            Type = type;
            DefaultValue = defaultValue;
            HasDefault = hasDefault;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append(Name);
            result.Append(" (");
            result.Append(Type);
            result.Append(")");
            if (HasDefault)
            {
                result.Append(" (Default=");
                if (DefaultValue == null)
                {
                    result.Append("null");
                }
                else
                {
                    result.Append("'");
                    result.Append(DefaultValue);
                    result.Append("'");
                }
                result.Append(")");
            }

            return result.ToString();
        }

        public void WriteToXml(XmlWriter writer)
        {
            writer.WriteStartElement("Argument");
            {
                writer.WriteAttributeString("Name", Name);
                writer.WriteAttributeString("Type", Type);
                writer.WriteAttributeString("HasDefault", HasDefault.ToString());
                if (HasDefault && DefaultValue != null)
                    writer.WriteAttributeString("DefaultValue", DefaultValue);
            }
            writer.WriteEndElement();
        }
    }
}
