using System;
using System.Text;
using System.Xml;

namespace Roomie.Desktop.Engine
{
    public class DocumentationWriter
    {
        private static RoomieCommandLibrary getCommands(string binFolder)
        {
            RoomieCommandLibrary library = new RoomieCommandLibrary();

            // Add the core commands
            library.AddCommandsFromAssembly(typeof(RoomieEngine).Assembly);
            
            // Add the extension commands
            library.AddCommandsFromPluginFolder(binFolder);
            
            return library;
        }

        public static void WriteCommandDocumentation(XmlWriter writer, RoomieCommandLibrary library, string commandsDocumentationClass)
        {
            writer.WriteStartElement("div");
            {
                writer.WriteAttributeString("class", commandsDocumentationClass);

                foreach(RoomieCommand command in library)
                {
                    writer.WriteStartElement("div");
                    {
                        writer.WriteAttributeString("class", "command");

                        //write group
                        writer.WriteStartElement("div");
                        {
                            writer.WriteAttributeString("class", "group");

                            writer.WriteString(command.Group);
                            writer.WriteString(".");
                        }
                        writer.WriteEndElement();

                        //write name
                        writer.WriteStartElement("div");
                        {
                            writer.WriteAttributeString("class", "name");

                            writer.WriteString(command.Name);
                        }
                        writer.WriteEndElement();

                        //write extension name and version
                        writer.WriteStartElement("div");
                        {
                            writer.WriteAttributeString("class", "extension");

                            writer.WriteString(command.ExtensionName);

                            if (command.ExtensionVersion != null)
                            {
                                writer.WriteString(" v");
                                writer.WriteString(command.ExtensionVersion.ToString());
                            }
                        }
                        writer.WriteEndElement();                        

                        if(!String.IsNullOrEmpty(command.Description))
                        {
                            writer.WriteStartElement("div");
                            {
                                writer.WriteAttributeString("class", "description");

                                writer.WriteString(command.Description);
                            }
                            writer.WriteEndElement();
                        }

                        writer.WriteStartElement("table");
                        {
                            writer.WriteAttributeString("class", "arguments");

                            //write argument table headers
                            writer.WriteStartElement("tr");
                            {
                                writer.WriteElementString("th", "Argument Name");
                                writer.WriteElementString("th", "Argument Type");
                                writer.WriteElementString("th", "Argument Default");
                            }
                            writer.WriteEndElement();



                            foreach(RoomieCommandArgument argument in command.Arguments)
                            {
                            writer.WriteStartElement("tr");
                            {
                                writer.WriteElementString("td", argument.Name);
                                writer.WriteElementString("td", argument.Type);
                                if (argument.HasDefault)
                                {
                                    if (argument.DefaultValue == null)
                                        writer.WriteElementString("td", "null");
                                    else if (argument.DefaultValue.Equals(String.Empty))
                                        writer.WriteElementString("td", "empty string");
                                    else
                                        writer.WriteElementString("td", "\"" + argument.DefaultValue + "\""); //TODO: encode?
                                }
                                else
                                    writer.WriteElementString("td", "no default");
                            }
                            writer.WriteEndElement();
                            }
                        }
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }
            }
            writer.WriteEndElement();
        }
        public static void WriteCommandDocumentation(XmlWriter writer, string binFolder, string commandDocumentationClass)
        {
            WriteCommandDocumentation(writer, getCommands(binFolder), commandDocumentationClass);
        }
        public static string WriteCommandDocumentation(string binFolder, string commandDocumentationClass)
        {
            StringBuilder builder = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            XmlWriter writer = XmlWriter.Create(builder, settings);

            WriteCommandDocumentation(writer, getCommands(binFolder), commandDocumentationClass);

            writer.Close();
            return builder.ToString();
        }
    }
}
