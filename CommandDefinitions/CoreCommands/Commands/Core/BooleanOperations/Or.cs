using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core.BooleanOperations
{
    [Parameter("Value1", BooleanParameterType.Key)]
    [Parameter("Value2", BooleanParameterType.Key)]
    [StringParameter("ResultName")]
    [Description("Sets the variable in ResultName to the result of ${Value1}||${Value2}.")]
    public class Or : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var scope = context.Scope;

            bool value1 = scope.GetBoolean("Value1");
            bool value2 = scope.GetBoolean("Value2");
            string resultName = scope.GetValue("ResultName");

            bool result = value1 || value2;

            if (scope.HigherScope.ContainsVariableInScope(resultName))
                scope.HigherScope.ModifyVariableValue(resultName, result.ToString());
            else
                scope.HigherScope.DeclareVariable(resultName, result.ToString());
        }
    }
}
