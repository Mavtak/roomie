using System;
using System.Collections.Generic;
using System.Xml;
using Roomie.Common.Exceptions;
using Roomie.Common.ScriptingLanguage.Exceptions;

namespace Roomie.Common.ScriptingLanguage
{
    public static class Common
    {
        //TODO: load into XElement instead
        internal static List<System.Xml.XmlNode> GetXml(string xmlText)
        {
            var xmlDocument = new System.Xml.XmlDocument();

            try
            {
                string toLoad = "<dummy>" + xmlText + "</dummy>";
                xmlDocument.LoadXml(toLoad);
            }
            catch (XmlException exception)
            {
                throw new RoomieScriptSyntaxErrorException(exception);
            }
            catch (Exception unexpectedException)
            {
                throw new UnexpectedException(unexpectedException);
            }

            var rootNode = xmlDocument.ChildNodes[xmlDocument.ChildNodes.Count - 1];

            if (rootNode.InnerText == rootNode.InnerXml)
            {
                throw new RoomieScriptSyntaxErrorException("this is not xml.  It's just plain text");
            }

            var result = new List<System.Xml.XmlNode>(rootNode.ChildNodes.Count);

            foreach (XmlNode node in rootNode.ChildNodes)
            {
                result.Add(node);
            }

            return result;
        }

        //TODO: load into XElement instead
        internal static XmlNode LoadXml(string path)
        {
            var xmlDocument = new System.Xml.XmlDocument();
            try
            {
                xmlDocument.Load(path);
            }
            catch (XmlException xmlException)
            {
                throw new RoomieScriptSyntaxErrorException(xmlException);
            }
                //TODO: catch IO exceptions
            catch (Exception unexpectedException)
            {
                throw new UnexpectedException(unexpectedException);
            }
            var rootNode = xmlDocument.ChildNodes[xmlDocument.ChildNodes.Count - 1];

            return rootNode;
        }

        public static Version LibraryVersion
        {
            get
            {
                return InternalLibraryVersion.GetLibraryVersion();
            }
        }
    }
}
