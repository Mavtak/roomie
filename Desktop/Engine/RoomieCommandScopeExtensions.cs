using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Exceptions;

namespace Roomie.Desktop.Engine
{
    public static class RoomieCommandScopeExtensions
    {
        public static KeyValuePair<string, string>[] FindGivenValues(this RoomieCommandScope scope)
        {
            var variables = scope.Variables;

            var result = variables
                .Select(x => new KeyValuePair<string, string>(x, scope.GetLiteralValue(x)))
                .ToArray();

            return result;
        }

        public static RoomieCommandArgument[] FindMissingArguments(this RoomieCommandScope scope, IEnumerable<RoomieCommandArgument> arguments)
        {
            var result = new List<RoomieCommandArgument>();

            foreach (var argument in arguments)
            {
                if (!argument.HasDefault & !scope.VariableDefinedInThisScope(argument.Name))
                {
                    result.Add(argument);
                }
            }

            return result.ToArray();
        }

        public static KeyValuePair<string, string>[] ApplyDefaults(this RoomieCommandScope scope, IEnumerable<RoomieCommandArgument> arguments)
        {
            var result = new List<KeyValuePair<string, string>>();

            foreach (var argument in arguments)
            {
                if (!scope.VariableDefinedInThisScope(argument.Name))
                {
                    scope.DeclareVariable(argument.Name, argument.DefaultValue);

                    result.Add(new KeyValuePair<string, string>(argument.Name, argument.DefaultValue));
                }
            }

            return result.ToArray();
        }

        public static RoomieCommandArgument[] FindMistypedArguments(this RoomieCommandScope scope, IEnumerable<RoomieCommandArgument> arguments)
        {
            var result = new List<RoomieCommandArgument>();

            foreach (var argument in arguments)
            {
                var value = scope.GetValue(argument.Name);
                var type = argument.Type;
                var isValid = type.Validate(value);

                if (!isValid)
                {
                    result.Add(argument);
                }
            }

            return result.ToArray();
        }

        public static void PrepareForCall(this RoomieCommandScope scope, ICommandSpecification command, RoomieCommandInterpreter interpreter)
        {
            var givenValues = scope.FindGivenValues();
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

        private static string BuilCommandCall(string fullName, KeyValuePair<string, string>[] givenValues, KeyValuePair<string, string>[] defaultedValues)
        {
            var result = new StringBuilder();

            result.Append(fullName);

            foreach (var pair in givenValues)
            {
                result.Append(" ");
                result.Append(pair.Key);
                result.Append("=\"");
                result.Append(pair.Value);
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
