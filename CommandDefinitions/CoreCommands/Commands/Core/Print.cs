using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core
{
    [StringParameter("Text")]
    public class Print : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var interpreter = context.Interpreter;

            interpreter.WriteEvent(context.ReadParameter("Text").Value);
        }
    }
}
