using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core
{
    [Description("This command closes Roomie.")]
    public class Exit : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var engine = context.Engine;
            var interpreter = context.Interpreter;

            interpreter.WriteEvent("Shutting down...");
            engine.Shutdown();
        }
    }
}
