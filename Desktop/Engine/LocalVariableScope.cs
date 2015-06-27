using System.Collections.Generic;
using System.Text.RegularExpressions;
using Roomie.Desktop.Engine.Exceptions;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.Desktop.Engine
{
    public class LocalVariableScope
    {
        public const string VariableNamePattern = "[A-Za-z0-9-_ ]+?";
        public const string VariableFormatPattern = @"\$\{" + VariableNamePattern + @"\}";

        private readonly Dictionary<string, VariableParameter> _variables;

        public LocalVariableScope()
        {
            _variables = new Dictionary<string, VariableParameter>();
        }

        public ICollection<string> Names
        {
            get
            {
                return _variables.Keys;
            }
        }

        public bool IsValidVariableName(string name)
        {
            var pattern = new Regex(@"\A" + VariableNamePattern + @"\Z");

            return pattern.IsMatch(name);
        }

        public bool ContainsLocalVariable(string name)
        {
            return _variables.ContainsKey(name);
        }

        public void DeclareLocalVariable(string name, string value)
        {
            if (!IsValidVariableName(name))
            {
                throw new VariableException("\"" + name + "\" is not a valid variable name");
            }

            lock (this)
            {
                if (ContainsLocalVariable(name))
                {
                    throw new VariableException("Variable \"" + name + "\" already exists.");
                }

                _variables.Add(name, new VariableParameter(name, value));
            }
        }

        public VariableParameter TryGetLocalVariable(string name)
        {
            lock (this)
            {
                if (_variables.ContainsKey(name))
                {
                    return _variables[name];
                }

                return null;
            }
        }

        public void ResetLocalScope()
        {
            _variables.Clear();
        }
    }
}
