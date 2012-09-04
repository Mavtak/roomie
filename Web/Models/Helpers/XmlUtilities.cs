using System.Xml;

namespace Roomie.Web.Helpers
{
    //TODO: pull this into Roomie.Common?
    public static class XmlUtilities
    {
        public static XmlNode StringToXml(string xmlText)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xmlText);
            return xmldoc.ChildNodes[xmldoc.ChildNodes.Count - 1];
        }

        public static XmlNode StringToXml(string xmlText, string backupOuterTag)
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
