﻿using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core
{
    [StringParameter("Name")]
    [StringParameter("Value")]
    [BooleanParameter("Global", false)]
    [BooleanParameter("Literal", false)]
    public class ModifyVariable : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var globalScope = context.GlobalScope;
            var interpreter = context.Interpreter;
            var scope = context.Scope;

            string name = scope.ReadParameter("Name").Value;

            bool literal = scope.ReadParameter("Literal").ToBoolean();
            string value;
            if(literal)
                value = scope.GetVariable("Value").Value;
            else
                value = scope.ReadParameter("Value").Value;
            bool global = scope.ReadParameter("Global").ToBoolean();

            if (global)
                globalScope.GetVariable(name).Update(value);
            else
                scope.Parent.GetVariable(name).Update(value);
        }
    }
}
