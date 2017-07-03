using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Exceptions;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.Desktop.Engine
{
    public static class RoomieCommandScopeExtensions
    {
        public static VariableParameter[] FindGivenValues(this LocalVariableScope scope)
        {
            var variables = scope.Names;

            var result = variables
                .Select(scope.TryGetVariable)
                .ToArray();

            return result;
        }

        public static RoomieCommandArgument[] FindMissingArguments(this HierarchicalVariableScope scope, IEnumerable<RoomieCommandArgument> arguments)
        {
            var result = new List<RoomieCommandArgument>();

            foreach (var argument in arguments)
            {
                if (!argument.HasDefault & !scope.Local.ContainsVariable(argument.Name))
                {
                    result.Add(argument);
                }
            }

            return result.ToArray();
        }

        public static KeyValuePair<string, string>[] ApplyDefaults(this HierarchicalVariableScope scope, IEnumerable<RoomieCommandArgument> arguments)
        {
            var result = new List<KeyValuePair<string, string>>();

            foreach (var argument in arguments)
            {
                if (!scope.Local.ContainsVariable(argument.Name))
                {
                    scope.Local.DeclareVariable(argument.Name, argument.DefaultValue);

                    result.Add(new KeyValuePair<string, string>(argument.Name, argument.DefaultValue));
                }
            }

            return result.ToArray();
        }

        public static RoomieCommandArgument[] FindMistypedArguments(this HierarchicalVariableScope scope, IEnumerable<RoomieCommandArgument> arguments)
        {
            var result = new List<RoomieCommandArgument>();

            foreach (var argument in arguments)
            {
                var variable = scope.GetVariable(argument.Name);
                var value = variable.Interpolate(scope);
                var type = argument.Type;
                var isValid = type.Validate(value);

                if (!isValid)
                {
                    result.Add(argument);
                }
            }

            return result.ToArray();
        }

        public static void PrepareForCall(this HierarchicalVariableScope scope, ICommandSpecification command, RoomieCommandInterpreter interpreter)
        {
            var givenValues = scope.Local.FindGivenValues();
            var missingArguments = scope.FindMissingArguments(command.Arguments);

            if (missingArguments.Length > 0)
            {
                throw new MissingArgumentsException(missingArguments);
            }

            var defaultedValues = scope.ApplyDefaults(command.Arguments);

            if (interpreter.Engine.PrintCommandCalls)
            {
                var call = BuilCommandCall(command.Group + "." + command.Name, givenValues, defaultedValues);
                interpreter.WriteEvent(call);
            }

            var mistypedArguments = scope.FindMistypedArguments(command.Arguments);

            if (mistypedArguments.Any())
            {
                foreach (var argument in mistypedArguments)
                {
                    interpreter.WriteEvent(argument.Type.ValidationMessage(argument.Name));
                }

                throw new MistypedArgumentException(mistypedArguments);
            }
        }

        private static string BuilCommandCall(string fullName, VariableParameter[] givenParameters, KeyValuePair<string, string>[] defaultedValues)
        {
            var result = new StringBuilder();

            result.Append(fullName);

            foreach (var parameter in givenParameters)
            {
                result.Append(" ");
                result.Append(parameter.Name);
                result.Append("=\"");
                result.Append(parameter.Value);
                result.Append("\"");
            }

            if (defaultedValues.Any())
            {
                result.Append("(with defaults:");

                foreach (var pair in defaultedValues)
                {
                    result.Append(" ");
                    result.Append(pair.Key);
                    result.Append("=\"");
                    result.Append(pair.Value);
                    result.Append("\"");
                }

                result.Append(")");
            }

            return result.ToString();
        }
    }
}
