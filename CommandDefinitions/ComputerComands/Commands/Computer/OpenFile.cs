using System;
using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.CommandDefinitions.ComputerCommands.Commands.Computer
{
    [StringParameter("Path")]
    [StringParameter("Arguments", null)]
    [BooleanParameter("AbortOnError", true)]
    [Description("This command opens a file on the computer.  The file can be an executable (.exe file) or a data file.")]
    public class OpenFile : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            string path = context.ReadParameter("Path").Value;
            bool abortOnError = context.ReadParameter("AbortOnError").ToBoolean();

            try
            {
                System.Diagnostics.Process.Start(path);
            }
            catch(Exception e)
            {
                if (abortOnError)
                {
                    throw new RoomieRuntimeException("Error opening file: " + e.Message);
                }
                else
                {
                    //TODO:
                }
            }
        }
    }
}
