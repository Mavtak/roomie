﻿using System;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core.DateTimeOperations
{
    [DateTimeParameter("DateTime")]
    [TimeSpanParameter("TimeSpan")]
    [StringParameter("ResultName")]
    [Description("Sets the variable in ResultName to the result of ${Value1}-${Value2}.")]
    public class SubtractTimeSpan : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var scope = context.Scope;

            DateTime dateTime = scope.GetValue("DateTime").ToDateTime();
            TimeSpan timeSpan = scope.GetValue("TimeSpan").ToTimeSpan();
            string resultName = scope.GetValue("ResultName");

            DateTime result = dateTime.Subtract(timeSpan);

            if (scope.HigherScope.ContainsVariableInScope(resultName))
                scope.HigherScope.ModifyVariableValue(resultName, TimeUtils.DateTimeToString(result));
            else
                scope.HigherScope.DeclareVariable(resultName, TimeUtils.DateTimeToString(result));
        }
    }
}
