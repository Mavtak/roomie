﻿using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core.BooleanOperations
{
    [BooleanParameter("Value1")]
    [BooleanParameter("Value2")]
    [StringParameter("ResultName")]
    [Description("Sets the variable in ResultName to the result of ${Value1}&&${Value2}.")]
    public class And : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var scope = context.Scope;

            bool value1 = scope.ReadParameter("Value1").ToBoolean();
            bool value2 = scope.ReadParameter("Value2").ToBoolean();
            string resultName = scope.ReadParameter("ResultName").Value;

            bool result = value1 && value2;

            if (scope.HigherScope.ContainsVariableInScope(resultName))
                scope.HigherScope.GetVariable(resultName).Update(result.ToString());
            else
                scope.HigherScope.DeclareVariable(resultName, result.ToString());
        } 
    }
}
