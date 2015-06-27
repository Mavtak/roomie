using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core
{
    [BooleanParameter("LiteralValues", false)]
    [Description("Prints a list of all variables in the scope higherarchy.")]
    public class ListVariables : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var interpreter = context.Interpreter;
            var scope = context.Scope;

            bool literalValues = scope.ReadParameter("LiteralValues").ToBoolean();


            RoomieCommandScope currentScope = scope;
            interpreter.WriteEvent("---");
            while (currentScope != null)
            {
                foreach (string name in currentScope.Names)
                {
                    if(literalValues)
                        interpreter.WriteEvent(name + ": " + scope.GetVariable(name).Value);
                    else
                        interpreter.WriteEvent(name + ": " + scope.ReadParameter(name));
                }
                interpreter.WriteEvent("---");
                currentScope = currentScope.Parent;
            }
        }
    }
}
