using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core
{
    [Parameter("Text", "String")]
    public class Print : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var interpreter = context.Interpreter;
            var scope = context.Scope;

            interpreter.WriteEvent(scope.GetValue("Text"));
        }
    }
}
