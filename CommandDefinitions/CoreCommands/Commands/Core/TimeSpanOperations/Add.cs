using System;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core.TimeSpanOperations
{
    [Parameter("Value1", "TimeSpan")]
    [Parameter("Value2", "TimeSpan")]
    [Parameter("ResultName", StringParameterType.Key)]
    [Description("Sets the variable in ResultName to the result of ${Value1}+${Value2}.")]
    public class Add : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var scope = context.Scope;

            TimeSpan value1 = scope.GetTimeSpan("Value1");
            TimeSpan value2 = scope.GetTimeSpan("Value2");
            string resultName = scope.GetValue("ResultName");

            TimeSpan result = value1.Add(value2);

            if (scope.HigherScope.ContainsVariableInScope(resultName))
                scope.HigherScope.ModifyVariableValue(resultName, TimeUtils.TimeSpanToString(result));
            else
                scope.HigherScope.DeclareVariable(resultName, TimeUtils.TimeSpanToString(result));
        }
    }
}
