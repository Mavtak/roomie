using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
            var scope = context.Scope;

            var group = scope.ReadParameter("Group").Value;

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
