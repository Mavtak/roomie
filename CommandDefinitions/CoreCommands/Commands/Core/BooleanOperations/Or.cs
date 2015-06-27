﻿using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core.BooleanOperations
{
    [BooleanParameter("Value1")]
    [BooleanParameter("Value2")]
    [StringParameter("ResultName")]
    [Description("Sets the variable in ResultName to the result of ${Value1}||${Value2}.")]
    public class Or : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var scope = context.Scope;

            bool value1 = scope.ReadParameter("Value1").ToBoolean();
            bool value2 = scope.ReadParameter("Value2").ToBoolean();
            string resultName = scope.ReadParameter("ResultName").Value;

            bool result = value1 || value2;

            if (scope.Parent.Local.ContainsLocalVariable(resultName))
                scope.Parent.GetVariable(resultName).Update(result.ToString());
            else
                scope.Parent.Local.DeclareLocalVariable(resultName, result.ToString());
        }
    }
}
