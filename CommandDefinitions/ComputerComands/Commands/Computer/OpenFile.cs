using System;
using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.ComputerCommands.Commands.Computer
{
    [Parameter("Path", "String")]
    [Parameter("Arguments", "String", null)]
    [Parameter("AbortOnError", "Boolean", "True")]
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
