using System.Text;
using System.Xml;
using Roomie.Common;

namespace Roomie.Web.Website.Helpers
{
    //TODO: pull into Roomie.Common.ScriptingLanguage?
    public class ScriptFormatter
    {
        private string open;
        private string close;

        private string newLine;
        private string indent;
        private string singleSpace;
        private string openBracket;
        private string closeBracket;

        private string openElementFormat;
        private string closeElementFormat;

        private string openAttributeName;
        private string closeAttributeName;

        private string openValue;
        private string closeValue;

        private string openComment;
        private string closeComment;

        private ScriptFormatter()
        {
        }

        private static ScriptFormatter htmlFormatter;
        public static ScriptFormatter HtmlFormatter
        {
            get
            {
                if (htmlFormatter == null)
                    htmlFormatter = CreateBoxedHtmlFormatter();
                return htmlFormatter;
            }
        }

        private static ScriptFormatter CreateHtmlFormatter()
        {
            ScriptFormatter toReturn = new ScriptFormatter();

            toReturn.open = "<span style=\"color:blue; font-family : monospace; white-space:nowrap;\" >";
            toReturn.close = "</span>";

            toReturn.newLine = "<br />";
            toReturn.indent = "&nbsp;&nbsp;";
            toReturn.singleSpace = "&nbsp;";
            toReturn.openBracket = "&lt;";
            toReturn.closeBracket = "&gt;";

            toReturn.openElementFormat = "<span style=\"color:maroon\" >";
            toReturn.closeElementFormat = "</span>";

            toReturn.openAttributeName = "<span style=\"color:red\">";
            toReturn.closeAttributeName = "</span>";

            toReturn.openValue = "<span style=\"color:black\" ><b>";
            toReturn.closeValue = "</b></span>";

            toReturn.openComment = "<span style=\"font-style:italic; color:grey;\">&lt;!--" + toReturn.singleSpace;
            toReturn.closeComment = toReturn.singleSpace + "--&gt;</span>";

            return toReturn;
        }

        public static ScriptFormatter CreatePlainTextFormatter()
        {
            ScriptFormatter toReturn = new ScriptFormatter();

            toReturn.open = "";
            toReturn.close = "";

            toReturn.newLine = "\n";
            toReturn.indent = "  ";
            toReturn.singleSpace = " ";
            toReturn.openBracket = "<";
            toReturn.closeBracket = ">";

            toReturn.openElementFormat = "";
            toReturn.closeElementFormat = "";

            toReturn.openAttributeName = "";
            toReturn.closeAttributeName = "";

            toReturn.openValue = "";
            toReturn.closeValue = "";

            toReturn.openComment = toReturn.openBracket + "!--" + toReturn.singleSpace;
            toReturn.closeComment = toReturn.singleSpace + "--" + toReturn.closeBracket;

            return toReturn;
        }
        public static ScriptFormatter CreateBoxedHtmlFormatter(string width, string height)
        {
            ScriptFormatter toReturn = CreateHtmlFormatter();

            toReturn.open = "<div style=\"width: " + width + "; height: " + height + "; overflow: auto;\">" + toReturn.open;
            toReturn.close = toReturn.close + "</div>";

            return toReturn;
        }
        public static ScriptFormatter CreateBoxedHtmlFormatter()
        {
            return CreateBoxedHtmlFormatter("500px", "");
        }

        public string Format(string node)
        {
            return Format(XmlUtilities.StringToXml(node));
        }
        public string Format(XmlNode node)
        {
            if (node == null)
                return "";

            StringBuilder builder = new StringBuilder();
            builder.Append(open);
            writeFormat(builder, node, 0);
            builder.Append(close);
            return builder.ToString().ToString();
        }
        private void writeFormat(StringBuilder builder, XmlNode node, int numIndents)
        {
            string indents = getIndents(numIndents);

            if (node.Name == "#text")
            {
                builder.Append(indents + indent + openValue + node.InnerText.Replace("\r\n", newLine + indents + indent).Replace("\n", newLine + indents + indent) + closeValue + newLine);
                return;
            }

            if (node.Name == "#comment")
            {
                builder.Append(indents + openComment + node.InnerText.Replace("\r\n", newLine + indents).Replace("\n", newLine + indents) + closeComment + newLine);
                return;
            }

            builder.Append(indents + openBracket + openElementFormat + node.Name + closeElementFormat);

            if (node.Attributes != null)
                foreach (XmlAttribute attribute in node.Attributes)
                    builder.Append(singleSpace + openAttributeName + attribute.Name + closeAttributeName + "=\"" + openValue + attribute.Value + closeValue + "\"");



            if (node.ChildNodes.Count == 0)
            {
                builder.Append(singleSpace + "/" + closeBracket + newLine);
                return;
            }
            else
                builder.Append(closeBracket + newLine);

            foreach (XmlNode childNode in node.ChildNodes)
                writeFormat(builder, childNode, numIndents + 1);

            builder.Append(indents + openBracket + "/" + openElementFormat + node.Name + closeElementFormat + closeBracket + newLine);
        }

        private string getIndents(int numIndents)
        {
            StringBuilder builder = new StringBuilder();

            for (int x = 0; x < numIndents; x++)
                builder.Append(indent);
            return builder.ToString();
        }
    }
}