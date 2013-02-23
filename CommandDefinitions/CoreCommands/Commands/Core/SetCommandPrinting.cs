﻿using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core
{
    [BooleanParameter("Value", "True")]
    public class SetCommandPrinting : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var engine = context.Engine;
            var interpreter = context.Interpreter;
            var scope = context.Scope;

            bool value = scope.GetBoolean("Value");

            engine.PrintCommandCalls = value;
        }
    }
}
