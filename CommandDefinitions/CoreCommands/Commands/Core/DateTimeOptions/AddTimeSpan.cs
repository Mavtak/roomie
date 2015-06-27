using System;
using Roomie.Common;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core.DateTimeOperations
{
    [DateTimeParameter("DateTime")]
    [TimeSpanParameter("TimeSpan")]
    [StringParameter("ResultName")]
    [Description("Sets the variable in ResultName to the result of ${Value1}+${Value2}.")]
    public class AddTimeSpan : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var scope = context.Scope;

            DateTime dateTime = scope.ReadParameter("DateTime").ToDateTime();
            TimeSpan timeSpan = scope.ReadParameter("TimeSpan").ToTimeSpan();
            string resultName = scope.ReadParameter("ResultName").Value;

            DateTime result = dateTime.Add(timeSpan);

            if (scope.HigherScope.ContainsVariableInScope(resultName))
                scope.HigherScope.GetVariable(resultName).Update(TimeUtils.DateTimeToString(result));
            else
                scope.HigherScope.DeclareVariable(resultName, TimeUtils.DateTimeToString(result));
        }
    }
}
