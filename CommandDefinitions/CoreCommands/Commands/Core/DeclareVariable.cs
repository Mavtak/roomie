﻿using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core
{
    [StringParameter("Name")]
    [StringParameter("Value", null)]//TODO: should default value be an empty string instead?
    [BooleanParameter("Global", false)]
    [Description("This command declares a variable with a given value.  \"Global\" creates the element in global scope.")]
    public class DeclareVariable : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var globalScope = context.GlobalScope;
            var scope = context.Scope;

            string name = scope.ReadParameter("Name").Value;
            string value = scope.ReadParameter("Value").Value;
            bool global = scope.ReadParameter("Global").ToBoolean();

            if (global)
                globalScope.DeclareVariable(name, value);
            else
                scope.Parent.DeclareVariable(name, value);
        }
    }
}
