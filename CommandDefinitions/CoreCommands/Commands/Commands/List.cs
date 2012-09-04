using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Commands
{
    [Description("This command lists available Roomie commands.")]
    [Parameter("Group", "String", "")]
    public class List : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            //TODO: make this more efficient
            var commands = context.CommandLibrary;
            var interpreter = context.Interpreter;
            var scope = context.Scope;

            var group = scope.GetValue("Group");

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
