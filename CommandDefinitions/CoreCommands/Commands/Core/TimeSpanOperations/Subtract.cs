using System;
using Roomie.Common;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core.TimeSpanOperations
{
    [TimeSpanParameter("Value1")]
    [TimeSpanParameter("Value2")]
    [StringParameter("ResultName")]
    [Description("Sets the variable in ResultName to the result of ${Value1}-${Value2}.")]
    public class Subtract : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var scope = context.Scope;

            TimeSpan value1 = scope.ReadParameter("Value1").ToTimeSpan();
            TimeSpan value2 = scope.ReadParameter("Value2").ToTimeSpan();
            string resultName = scope.ReadParameter("ResultName").Value;

            TimeSpan result = value1.Subtract(value2);

            scope.Parent.Local.SetVariable(resultName, result.ToString());
        }
    }
}
