using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core
{
    public class PrintStats : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var commandLibrary = context.CommandLibrary;
            var interpreter = context.Interpreter;

            var subinterpreter = interpreter.GetSubinterpreter();

            interpreter.WriteEvent("Stats:");
            interpreter.WriteEvent("--Version: " + Common.LibraryVersion.ToString());

            foreach (var command in commandLibrary)
            {
                if (command.Name.Equals("PrintStats") && !command.FullName.Equals(this.FullName))
                {
                    var scriptCommand = command.BlankCommandCall();
                    subinterpreter.CommandQueue.Add(scriptCommand);
                    subinterpreter.ProcessQueue();
                }
            }
        }
    }
}
