using System.Text;
using System.Xml;

namespace Roomie.Common.ScriptingLanguage
{
    public class ScriptCommand
    {
        public string FullName { get; private set; }
        public ScriptCommandParameters Parameters { get; private set; }
        public ScriptCommandList InnerCommands { get; private set; }
        public string OriginalText { get; private set; }

        public ScriptCommand(XmlNode node)
        {
            this.OriginalText = node.OuterXml;

            FullName = node.Name;

            Parameters = new ScriptCommandParameters();

            foreach (XmlAttribute attribute in node.Attributes)
            {
                var parameter = new ScriptCommandParameter(name: attribute.Name, value: attribute.Value);
                Parameters.Add(parameter);
            }

            InnerCommands = new ScriptCommandList(node.ChildNodes, node.OuterXml);
        }
        public ScriptCommand(string fullName, ScriptCommandParameters parameters = null)
        {
            this.FullName = fullName;
            this.Parameters = parameters ?? new ScriptCommandParameters();
            this.InnerCommands = new ScriptCommandList();
            this.OriginalText = "";//TODO: set something?
        }
        public override string ToString()
        {
            var result = new StringBuilder();

            result.Append(FullName);

            foreach (var parameter in Parameters)
            {
                result.Append(" ");
                result.Append(parameter.Name);
                result.Append("=\"");
                result.Append(parameter.Value);
                result.Append("\"");
            }

            return result.ToString();
        }
    }
}
