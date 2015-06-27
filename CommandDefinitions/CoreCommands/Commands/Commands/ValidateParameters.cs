using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Commands
{
    public class ValidateParameters : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var commands = context.CommandLibrary;
            var types = context.ArgumentTypes;
            var interpreter = context.Interpreter;

            foreach (var command in commands)
            {
                foreach (var argument in command.Arguments)
                {
                    if (!types.Contains(argument.Type))
                    {
                        interpreter.WriteEvent("Invalid type \"" + argument.Type + "\" for argument \"" + argument.Name + "\" in command " + command.FullName + ".");            
                    }
                }
            }
        }
    }
}
