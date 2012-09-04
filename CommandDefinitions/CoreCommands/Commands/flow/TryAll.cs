using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Flow
{
    class TryAll : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var interpreter = context.Interpreter;
            var originalCommand = context.OriginalCommand;
            var innerCommands = originalCommand.InnerCommands;

            var subinterpreter = interpreter.GetSubinterpreter();

            foreach (var command in innerCommands)
            {
                subinterpreter.CommandQueue.Add(command);
                var result = subinterpreter.ProcessQueue();
                if (!result)
                {
                    subinterpreter.WriteEvent("Previous command failed.  Continuing...");
                }
            }
        }
    }
}
