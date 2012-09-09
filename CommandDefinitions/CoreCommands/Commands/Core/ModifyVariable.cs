using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core
{
    [Parameter("Name", "String")]
    [Parameter("Value", "String")]
    [Parameter("Global", "Boolean", "False")]
    [Parameter("Literal", "Boolean", "False")]
    public class ModifyVariable : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var globalScope = context.GlobalScope;
            var interpreter = context.Interpreter;
            var scope = context.Scope;

            string name = scope.GetValue("Name");

            bool literal = scope.GetBoolean("Literal");
            string value;
            if(literal)
                value = scope.GetLiteralValue("Value");
            else
                value = scope.GetValue("Value");
            bool global = scope.GetBoolean("Global");

            if (global)
                globalScope.ModifyVariableValue(name, value);
            else
                scope.HigherScope.ModifyVariableValue(name, value);
        }
    }
}
