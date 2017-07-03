using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Commands
{
    public class ListGroups : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            foreach (var commandGroup in context.CommandLibrary.Groups)
            {
                context.Interpreter.WriteEvent(commandGroup);
            }
        }
    }
}
