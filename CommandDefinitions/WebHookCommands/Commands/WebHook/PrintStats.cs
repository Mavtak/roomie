using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.WebHookCommands.Commands.WebHook
{
    public class PrintStats : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var interpreter = context.Interpreter;

            interpreter.WriteEvent("WebHook Stats:");
            interpreter.WriteEvent("--Library Version: " + Common.LibraryVersion);
            interpreter.WriteEvent("--Web Communicator Version: " + WebCommunicator.Common.LibraryVersion);
        }
    }
}