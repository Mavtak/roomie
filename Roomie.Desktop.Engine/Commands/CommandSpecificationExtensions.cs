using System;
using System.Linq;
using System.Text;
using System.Xml;

namespace Roomie.Desktop.Engine.Commands
{
    public static class CommandSpecificationExtensions
    {
        public static void WriteToXml(this ICommandSpecification command, XmlWriter writer)
        {
            writer.WriteStartElement("Command");
            {
                writer.WriteAttributeString("PluginName", command.ExtensionName);
                writer.WriteAttributeString("Group", command.Group);
                writer.WriteAttributeString("Name", command.Name);

                if (!String.IsNullOrEmpty(command.Description))
                {
                    writer.WriteAttributeString("Description", command.Description);
                }

                foreach (RoomieCommandArgument argument in command.Arguments)
                {
                    argument.WriteToXml(writer);
                }
            }
            writer.WriteEndElement();
        }

        public static string ToConsoleFriendlyString(this ICommandSpecification command, bool isDynamic)
        {
            var builder = new StringBuilder();

            builder.Append("Command: ");
            builder.Append(command.Group + "." + command.Name);
            builder.AppendLine();

            if (!isDynamic)
            {
                builder.Append("Source: ");
                builder.Append(command.Source);
                builder.AppendLine();

                builder.Append("Version: ");
                builder.Append(command.ExtensionVersion);
                builder.AppendLine();
            }
            else
            {
                builder.Append("Dynamic Command");
                builder.AppendLine();
            }

            builder.Append("Description: ");
            builder.Append(command.Description);
            builder.AppendLine();

            builder.Append("Arguments:");

            if (!command.Arguments.Any())
            {
                builder.Append(" (none)");
            }
            else
            {
                foreach (var argument in command.Arguments)
                {
                    builder.AppendLine();
                    builder.Append("\t");
                    builder.Append(argument);
                }
            }
            builder.AppendLine();

            return builder.ToString();
        }

        public static string ToConsoleFriendlyString(this RoomieCommand command)
        {
            return ToConsoleFriendlyString(command, command.IsDynamic);
        }
    }
}
