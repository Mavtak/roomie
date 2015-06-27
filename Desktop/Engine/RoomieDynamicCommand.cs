using System.Collections.Generic;
using Roomie.Common.ScriptingLanguage;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.Desktop.Engine
{
    //TODO: could this be made internal?
    public class RoomieDynamicCommand : RoomieCommand
    {
        readonly ScriptCommandList subcommands;

        public RoomieDynamicCommand(string group, string name, List<RoomieCommandArgument> arguments, ScriptCommandList subcommands, string description)
            : base(new ReadOnlyCommandSpecification(
                name: name,
                group: group,
                description: description,
                source: "(dynamic command)",
                arguments: arguments
            ))
        {
            this.subcommands = subcommands;
        }

        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var interpreter = context.Interpreter;

            //TODO: define variables?

            var subInterpreter = interpreter.GetSubinterpreter();

            foreach (var parameter in context.OriginalCommand.Parameters)
            {
                subInterpreter.Scope.DeclareLocalVariable(parameter.Name, parameter.Value);
            }
            subInterpreter.CommandQueue.Add(subcommands);
            subInterpreter.ProcessQueue();
        }
    }
}
