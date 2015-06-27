using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core
{
    [BooleanParameter("Value", true)]
    public class SetCommandPrinting : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var engine = context.Engine;
            var interpreter = context.Interpreter;

            bool value = context.ReadParameter("Value").ToBoolean();

            engine.PrintCommandCalls = value;
        }
    }
}
