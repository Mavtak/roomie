using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core
{
    [Parameter("Value", "Boolean", "True")]
    public class SetCommandPrinting : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var engine = context.Engine;
            var interpreter = context.Interpreter;
            var scope = context.Scope;

            bool value = scope.GetBoolean("Value");

            engine.PrintCommandCalls = value;
        }
    }
}
