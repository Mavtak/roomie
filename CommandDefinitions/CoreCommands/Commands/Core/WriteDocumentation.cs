﻿using System;
using System.Text;
using System.Xml;
using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core
{
    [Parameter("Filename", "String", "./Documentation.xml")]
    public class WriteDocumentation : RoomieCommand
    {
        //TODO: eliminate use of System.Xml
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var commands = context.CommandLibrary;
            var interpreter = context.Interpreter;
            var scope = context.Scope;

            try
            {
                string filename = scope.GetValue("Filename");

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Encoding = Encoding.UTF8;
                settings.Indent = true;

                XmlWriter writer = XmlWriter.Create(filename, settings);
                writer.WriteStartDocument();
                commands.WriteToXml(writer, false);
                writer.WriteEndDocument();
                writer.Close();
            }
            catch (Exception exception)
            {
                throw new RoomieRuntimeException("Failed to write documentation: " + exception.Message, exception);
            }
        }
    }
}
