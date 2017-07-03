using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.EmailCommands.Commands
{
    [Group("Email")]
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
