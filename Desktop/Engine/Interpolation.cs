using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Roomie.Desktop.Engine.Exceptions;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.Desktop.Engine
{
    public static class Interpolation
    {
        public static IEnumerable<string> VariablesInString(string input)
        {
            var pattern = new Regex(RoomieCommandScope.VariableFormatPattern);
            foreach (Match match in pattern.Matches(input))
            {
                yield return match.Value;
            }
        }

        public static ReadOnlyParameter Interpolate(this VariableParameter givenVariable, RoomieCommandScope scope)
        {
            var builder = new StringBuilder(givenVariable.Value);

            List<string> variables;
            do
            {
                variables = VariablesInString(builder.ToString()).ToList();
                foreach (string variable in variables)
                {
                    var variableName = variable.Replace("${", "").Replace("}", "");
                    string replacement;
                    if (variableName != givenVariable.Name)
                    {
                        var matchedVariable = scope.GetVariable(variableName);

                        replacement = matchedVariable.Interpolate(scope).Value;
                    }
                    else if (scope.HigherScope != null)
                    {
                        var parentScope = scope.HigherScope;
                        var matchedVariable = parentScope.GetVariable(variableName);

                        replacement = matchedVariable.Interpolate(parentScope).Value;
                    }
                    else
                    {
                        replacement = null;
                    }

                    if (replacement == null)
                    {
                        //TODO: make this an explicit exception type
                        throw new VariableException("Variable \"" + variable + "\" not defined");
                    }
                    builder.Replace(variable, replacement);//TODO
                }
            } while (variables.Count != 0);

            return new ReadOnlyParameter(givenVariable.Name, builder.ToString());
        }
    }
}
