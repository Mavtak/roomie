﻿using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core
{
    [BooleanParameter("LiteralValues", false)]
    [Description("Prints a list of all variables in the scope higherarchy.")]
    public class ListVariables : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var interpreter = context.Interpreter;
            var scope = context.Scope;

            bool literalValues = context.ReadParameter("LiteralValues").ToBoolean();


            HierarchicalVariableScope currentScope = scope;
            interpreter.WriteEvent("---");
            while (currentScope != null)
            {
                foreach (string name in currentScope.Local.Names)
                {
                    if(literalValues)
                        interpreter.WriteEvent(name + ": " + scope.GetVariable(name).Value);
                    else
                        interpreter.WriteEvent(name + ": " + context.ReadParameter(name));
                }
                interpreter.WriteEvent("---");
                currentScope = currentScope.Parent;
            }
        }
    }
}
