using System;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Flow
{
    [DateTimeParameter("Time")]
    public class WaitUntil : RoomieCommand
    {

        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var interpreter = context.Interpreter;
            var scope = context.Scope;

            DateTime target = scope.GetDateTime("Time");

            interpreter.WriteEvent("waiting for " + target);
            Common.WaitUntil(target);            
        }
    }
}
