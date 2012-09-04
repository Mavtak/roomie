using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    public class PrintStats : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var interpreter = context.Interpreter;

            interpreter.WriteEvent("HomeAutomation Stats:");
            interpreter.WriteEvent("--Library Version: " + Common.LibraryVersion);
        }
    }
}
