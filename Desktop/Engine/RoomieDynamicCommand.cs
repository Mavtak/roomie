using System;
using System.Collections.Generic;

using Roomie.Common.ScriptingLanguage;

namespace Roomie.Desktop.Engine
{
    //TODO: could this be made internal?
    public class RoomieDynamicCommand : RoomieCommand
    {
        readonly ScriptCommandList subcommands;

        public RoomieDynamicCommand(string group, string name, List<RoomieCommandArgument> arguments, ScriptCommandList subcommands, string description)
            : base(group, name, "(dynamic command)", new Version(0,0,0,0), arguments, true, description)
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
                subInterpreter.Scope.DeclareVariable(parameter.Name, parameter.Value);
            }
            subInterpreter.CommandQueue.Add(subcommands);
            subInterpreter.ProcessQueue();
        }
    }
}
