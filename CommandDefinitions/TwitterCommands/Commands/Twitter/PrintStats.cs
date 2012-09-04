using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.TwitterCommands.Commands.Twitter
{
    public class PrintStats : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var interpreter = context.Interpreter;

            interpreter.WriteEvent("Twitter Stats:");
            interpreter.WriteEvent("--Library Version: " + Common.LibraryVersion);
        }
    }
}