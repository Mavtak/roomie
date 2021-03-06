﻿using System;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Flow
{
    [DateTimeParameter("Time")]
    public class WaitUntil : RoomieCommand
    {

        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var interpreter = context.Interpreter;

            DateTime target = context.ReadParameter("Time").ToDateTime();

            interpreter.WriteEvent("waiting for " + target);
            Common.WaitUntil(target);            
        }
    }
}
