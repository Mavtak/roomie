using System.Collections.Generic;
using System.Linq;

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
    }
}
