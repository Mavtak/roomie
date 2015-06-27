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

            int value1 = scope.ReadParameter("Value1").ToInteger();
            int value2 = scope.ReadParameter("Value2").ToInteger();
            string resultName = scope.ReadParameter("ResultName").Value;

            int result = value1 + value2;

            if (scope.Parent.ContainsLocalVariable(resultName))
                scope.Parent.GetVariable(resultName).Update(result.ToString());
            else
                scope.Parent.DeclareLocalVariable(resultName, result.ToString());
        }
    }
}
