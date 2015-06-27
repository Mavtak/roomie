using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core
{
    [StringParameter("Name")]
    [StringParameter("Value")]
    [BooleanParameter("Global", false)]
    [BooleanParameter("Literal", false)]
    public class ModifyVariable : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var globalScope = context.GlobalScope;
            var interpreter = context.Interpreter;
            var scope = context.Scope;

            string name = scope.GetValue("Name").Value;

            bool literal = scope.GetValue("Literal").ToBoolean();
            string value;
            if(literal)
                value = scope.GetLiteralValue("Value");
            else
                value = scope.GetValue("Value").Value;
            bool global = scope.GetValue("Global").ToBoolean();

            if (global)
                globalScope.ModifyVariableValue(name, value);
            else
                scope.HigherScope.ModifyVariableValue(name, value);
        }
    }
}
