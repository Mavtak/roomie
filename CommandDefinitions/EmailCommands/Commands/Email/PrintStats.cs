using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.EmailCommands.Commands.Email
{
    public class PrintStats : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var interpreter = context.Interpreter;

            interpreter.WriteEvent("Email Stats:");
            interpreter.WriteEvent("--Library Version: " + Common.LibraryVersion);
        }
    }
}
