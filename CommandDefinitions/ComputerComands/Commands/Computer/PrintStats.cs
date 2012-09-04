using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.ComputerCommands.Commands.Computer
{
    class PrintStats : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var interpreter = context.Interpreter;

            interpreter.WriteEvent("WakeOnLAN Stats:");
            interpreter.WriteEvent("--Library Version: " + Common.LibraryVersion);
        }
    }
}