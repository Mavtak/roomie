using System;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core.DateTimeOperations
{
    [Parameter("DateTime", "DateTime")]
    [Parameter("TimeSpan", TimeSpanParameterType.Key)]
    [Parameter("ResultName", StringParameterType.Key)]
    [Description("Sets the variable in ResultName to the result of ${Value1}+${Value2}.")]
    public class AddTimeSpan : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var scope = context.Scope;

            DateTime dateTime = scope.GetDateTime("DateTime");
            TimeSpan timeSpan = scope.GetTimeSpan("TimeSpan");
            string resultName = scope.GetValue("ResultName");

            DateTime result = dateTime.Add(timeSpan);

            if (scope.HigherScope.ContainsVariableInScope(resultName))
                scope.HigherScope.ModifyVariableValue(resultName, TimeUtils.DateTimeToString(result));
            else
                scope.HigherScope.DeclareVariable(resultName, TimeUtils.DateTimeToString(result));
        }
    }
}
