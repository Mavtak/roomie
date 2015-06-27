using System.Xml;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Commands
{
    [StringParameter("Path")]
    [Description("Writes command documentation to an XML file.")]
    public class WriteDocumentation : RoomieCommand
    {
        //TODO: eliminate use of System.Xml
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var commands = context.CommandLibrary;
            var interpreter = context.Interpreter;
            var scope = context.Scope;

            var path = scope.ReadParameter("Path").Value;

            interpreter.WriteEvent("Writing...");

            var writer = XmlWriter.Create(path);
            commands.WriteToXml(writer, false);
            writer.Close();

            interpreter.WriteEvent("Done.");
        }
    }
}
