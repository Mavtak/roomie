using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core
{
    [Parameter("Name", "String")]
    [Parameter("Value", "String", null)]//TODO: should default value be an empty string instead?
    [Parameter("Global", "Boolean", "False")]
    [Description("This command declares a variable with a given value.  \"Global\" creates the element in global scope.")]
    public class DeclareVariable : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var engine = context.Engine;
            var interpreter = context.Interpreter;
            var scope = context.Scope;

            string name = scope.GetValue("Name");
            string value = scope.GetValue("Value");
            bool global = scope.GetBoolean("Global");

            if (global)
                engine.GlobalScope.DeclareVariable(name, value);
            else
                scope.HigherScope.DeclareVariable(name, value);
        }
    }
}
