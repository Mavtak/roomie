using System.Collections.Generic;
using System.Linq;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Commands
{
    [Description("This command lists available Roomie commands.")]
    [StringParameter("Group", "")]
    public class List : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            //TODO: make this more efficient
            var commands = context.CommandLibrary;
            var interpreter = context.Interpreter;

            var group = context.ReadParameter("Group").Value;

            IEnumerable<RoomieCommand> subset;

            if (string.IsNullOrEmpty(group))
            {
                subset = commands;
            }
            else
            {
                subset = from command in commands
                         where @group == ""
                            || command.Group == @group
                         select command;
            }
            foreach (var command in subset)
            {
                interpreter.WriteEvent(command.ToConsoleFriendlyString());
            }
        }
    }
}
