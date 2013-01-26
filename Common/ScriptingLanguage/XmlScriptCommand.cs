using System.Collections.Generic;
using System.Linq;
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

            InnerCommands = ScriptCommandList.FromText(node.InnerXml);
        }

        public XmlScriptCommand(string fullName, ScriptCommandParameters parameters = null)
        {
            this.FullName = fullName;
            this.Parameters = parameters ?? new ScriptCommandParameters();
            this.InnerCommands = new ScriptCommandList();
            this.OriginalText = "";//TODO: set something?
        }

        public static IEnumerable<IScriptCommand> FromNodes(IEnumerable<XmlNode> nodes)
        {
            foreach (XmlNode node in nodes)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    var result = new XmlScriptCommand(node);

                    yield return result;
                }
            }
        }

        public static IEnumerable<IScriptCommand> FromNodes(XmlNodeList nodes)
        {
            return FromNodes(nodes.OfType<XmlNode>());
        }

        public override string ToString()
        {
            return this.Format();
        }

    }
}
