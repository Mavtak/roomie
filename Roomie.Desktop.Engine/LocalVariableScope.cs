﻿using System.Collections.Generic;
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

        public bool ContainsVariable(string name)
        {
            return _variables.ContainsKey(name);
        }

        public void DeclareVariable(string name, string value)
        {
            if (!IsValidVariableName(name))
            {
                throw new VariableException("\"" + name + "\" is not a valid variable name");
            }

            lock (this)
            {
                if (ContainsVariable(name))
                {
                    throw new VariableException("Variable \"" + name + "\" already exists.");
                }

                _variables.Add(name, new VariableParameter(name, value));
            }
        }

        public VariableParameter TryGetVariable(string name)
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

        public void SetVariable(string name, string value)
        {
            lock (this)
            {
                var variable = TryGetVariable(name);

                if (variable != null)
                {
                    variable.Update(value);
                }
                else
                {
                    DeclareVariable(name, value);
                }
            }
        }

        public void Reset()
        {
            _variables.Clear();
        }
    }
}
