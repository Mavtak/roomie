using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Commands
{
    [Description("Lists the dynamic commands (command created with Command.Create)")]
    public class ListDynamic : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var commands = context.CommandLibrary;
            var interpreter = context.Interpreter;

            foreach (var command in commands)
            {
                if (command.IsDynamic)
                {
                    interpreter.WriteEvent(command.ToConsoleFriendlyString());
                }
            }
        }
    }
}
