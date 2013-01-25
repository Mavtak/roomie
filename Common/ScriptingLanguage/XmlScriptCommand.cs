using System.Text;
using System.Xml;

namespace Roomie.Common.ScriptingLanguage
{
    public class XmlScriptCommand : IScriptCommand
    {
        public string FullName { get; private set; }
        public ScriptCommandParameters Parameters { get; private set; }
        public ScriptCommandList InnerCommands { get; private set; }
        public string OriginalText { get; private set; }

        public XmlScriptCommand(XmlNode node)
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

        public XmlScriptCommand(string fullName, ScriptCommandParameters parameters = null)
        {
            this.FullName = fullName;
            this.Parameters = parameters ?? new ScriptCommandParameters();
            this.InnerCommands = new ScriptCommandList();
            this.OriginalText = "";//TODO: set something?
        }

        public override string ToString()
        {
            return this.Format();
        }

    }
}
