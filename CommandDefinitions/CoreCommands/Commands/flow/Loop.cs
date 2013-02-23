using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Flow
{
    [IntegerParameter("Times", "-1")]
    public class Loop : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var interpreter = context.Interpreter;
            var scope = context.Scope;
            var originalCommand = context.OriginalCommand;
            var innerCommands = originalCommand.InnerCommands;

            int times = scope.GetInteger("Times");

            while (times != 0)
            {
                var subInterpreter = interpreter.GetSubinterpreter();
                subInterpreter.CommandQueue.Add(innerCommands);
                bool success = subInterpreter.ProcessQueue();

                if (!success)
                {
                    throw new RoomieRuntimeException("A command in the loop failed.");
                }

                if (times > 0)
                    times--;
            }
        }
    }
}
