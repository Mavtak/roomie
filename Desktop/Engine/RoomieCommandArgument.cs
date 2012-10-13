using System;
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
            this.Name = name;
            this.Type = type;
            this.DefaultValue = defaultValue;
            this.HasDefault = hasDefault;
        }
        public RoomieCommandArgument(XmlNode node)
        {
            Name = node.Attributes["Name"].Value;
            Type = node.Attributes["Type"].Value;
            HasDefault = Convert.ToBoolean(node.Attributes["HasDefault"]);
            if (!HasDefault || node.Attributes["DefaultValue"] == null)
                DefaultValue = null;
            else
                DefaultValue = node.Attributes["DefaultValue"].Value;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
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
