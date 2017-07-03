using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;

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

            int value1 = context.ReadParameter("Value1").ToInteger();
            int value2 = context.ReadParameter("Value2").ToInteger();
            string resultName = context.ReadParameter("ResultName").Value;

            int result = value1 + value2;

            scope.Parent.Local.SetVariable(resultName, result.ToString());
        }
    }
}
