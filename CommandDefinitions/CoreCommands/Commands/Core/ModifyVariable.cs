using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;

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

            string name = context.ReadParameter("Name").Value;

            bool literal = context.ReadParameter("Literal").ToBoolean();
            string value;
            if(literal)
                value = scope.GetVariable("Value").Value;
            else
                value = context.ReadParameter("Value").Value;
            bool global = context.ReadParameter("Global").ToBoolean();

            if (global)
                globalScope.GetVariable(name).Update(value);
            else
                scope.Parent.GetVariable(name).Update(value);
        }
    }
}
