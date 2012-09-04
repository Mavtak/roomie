﻿using System;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Flow
{
    [Parameter("Duration", "TimeSpan")]
    [Description("This command waits for the specified amount of time.  Example duration is \"1 Second\" or \"120 Seconds\"")]
    public class Wait : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var interpreter = context.Interpreter;
            var scope = context.Scope;

            var duration = scope.GetTimeSpan("Duration");
            var target = DateTime.Now.Add(duration);

            interpreter.WriteEvent("waiting for " + target);
            Common.WaitUntil(target);
        }

        
    }
}
