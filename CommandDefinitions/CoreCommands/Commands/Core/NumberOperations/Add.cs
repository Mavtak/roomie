﻿using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core.NumberOpersations
{
    [IntegerParameter("Value1")]
    [IntegerParameter("Value2")]
    [StringParameter("ResultName")]
    [Description("Sets the variable in ResultName to the result of ${Value1}+${Value2}.")]
    public class Add : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var scope = context.Scope;

            int value1 = scope.GetValue("Value1").ToInteger();
            int value2 = scope.GetValue("Value2").ToInteger();
            string resultName = scope.GetValue("ResultName");

            int result = value1 + value2;

            if (scope.HigherScope.ContainsVariableInScope(resultName))
                scope.HigherScope.ModifyVariableValue(resultName, result.ToString());
            else
                scope.HigherScope.DeclareVariable(resultName, result.ToString());
        }
    }
}
