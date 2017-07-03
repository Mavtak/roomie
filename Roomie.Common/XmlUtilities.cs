using System.Xml;
using System.Xml.Linq;

namespace Roomie.Common
{
    public static class XmlUtilities
    {
        /// <summary>
        /// Pulled from http://blogs.msdn.com/b/ericwhite/archive/2008/12/22/convert-xelement-to-xmlnode-and-convert-xmlnode-to-xelement.aspx
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static XElement GetXElement(this XmlNode node)
        {
            XDocument xDoc = new XDocument();
            using (XmlWriter xmlWriter = xDoc.CreateWriter())
                node.WriteTo(xmlWriter);
            return xDoc.Root;
        }

        /// <summary>
        /// Pulled from http://blogs.msdn.com/b/ericwhite/archive/2008/12/22/convert-xelement-to-xmlnode-and-convert-xmlnode-to-xelement.aspx
        /// Modified:  changed `return xmlDoc;` to `return xmlDoc.LastChild;`.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static XmlNode GetXmlNode(this XElement element)
        {
            using (XmlReader xmlReader = element.CreateReader())
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlReader);
                return xmlDoc.LastChild;
            }
        }

        public static XmlNode StringToXml(this string xmlText)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xmlText);
            return xmldoc.ChildNodes[xmldoc.ChildNodes.Count - 1];
        }

        public static XmlNode StringToXml(this string xmlText, string backupOuterTag)
        {
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                xmldoc.LoadXml(xmlText);
            }
            catch
            {
                xmldoc.LoadXml("<" + backupOuterTag + ">" + xmlText + "</" + backupOuterTag + ">");
            }

            return xmldoc.ChildNodes[xmldoc.ChildNodes.Count - 1];
        }
    }
}
