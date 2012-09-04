using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core.NumberOpersations
{
    [Parameter("Value1", "Integer")]
    [Parameter("Value2", "Integer")]
    [Parameter("ResultName", "String")]
    [Description("Sets the variable in ResultName to the result of ${Value1}-${Value2}.")]
    public class Subtract : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var scope = context.Scope;

            int value1 = scope.GetInteger("Value1");
            int value2 = scope.GetInteger("Value2");
            string resultName = scope.GetValue("ResultName");

            int result = value1 - value2;

            if (scope.HigherScope.ContainsVariableInScope(resultName))
                scope.HigherScope.ModifyVariableValue(resultName, result.ToString());
            else
                scope.HigherScope.DeclareVariable(resultName, result.ToString());
        }
    }
}
