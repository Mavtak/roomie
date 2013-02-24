using System;
using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

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
            var scope = context.Scope;

            string path = scope.GetValue("Path");
            bool abortOnError = scope.GetBoolean("AbortOnError");

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
