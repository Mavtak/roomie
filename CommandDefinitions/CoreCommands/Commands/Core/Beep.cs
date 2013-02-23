﻿using System;
using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core
{
    [IntegerParameter("Frequency", 1000)]
    [TimeSpanParameter("Duration", "500 milliseconds")]
    [Description("This command produces a console beep at the given Frequency and Duration.  (Default values for both are provided.)")]
    public class Beep : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var scope = context.Scope;

            int frequency = scope.GetInteger("Frequency");
            TimeSpan duration = scope.GetTimeSpan("Duration");

            double ms = duration.TotalMilliseconds;

            if (ms > int.MaxValue)
                throw new RoomieRuntimeException("Duration for beep too long.  Can be at most " + new TimeSpan(0, 0, 0, 0, int.MaxValue).TotalDays + " days.");


            Console.Beep(frequency, (int)ms);
        }
    }
}
