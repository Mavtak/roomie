using Roomie.Common.Temperature;
using Roomie.Desktop.Engine.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Roomie.Desktop.Engine
{
    //TODO: clean this class up
    public class RoomieCommandScope
    {
        public const string VariableNamePattern = "[A-Za-z0-9-_ ]+?";
        public const string VariableFormatPattern = @"\$\{" + VariableNamePattern + @"\}";

        private readonly Dictionary<string, string> _variables;
        public RoomieCommandScope HigherScope { get; private set; }

        public RoomieCommandScope(RoomieCommandScope higherScope)
        {
            HigherScope = higherScope;
            _variables = new Dictionary<string, string>();
        }
        public RoomieCommandScope()
            : this(null)
        {
        }

        public ICollection<string> Names
        {
            get
            {
                return _variables.Keys;
            }
        }

        public RoomieCommandScope CreateLowerScope()
        {
            return new RoomieCommandScope(this);
        }

        public bool ContainsVariableInScope(string name)
        {
            return _variables.ContainsKey(name);
        }

        public void DeclareVariable(string name, string value)
        {
            if (!IsValidVariableName(name))
                throw new VariableException("\"" + name + "\" is not a valid variable name");

            lock (_variables)
            {
                if (ContainsVariableInScope(name))
                    throw new VariableException("Variable \"" + name + "\" already exists.");
                _variables.Add(name, value);
            }
        }
        public void DeclareVariable(string name)
        {
            DeclareVariable(name, null);
        }
        public void ReplaceVariable(string name, string value)
        {
            if (ContainsVariableInScope(name))
                ModifyVariableValue(name, value);
            else
                DeclareVariable(name, value);
        }


        public void ModifyVariableValue(string name, string value)
        {
            lock (_variables)
            {
                if (_variables.ContainsKey(name))
                {
                    _variables.Remove(name);
                    _variables.Add(name, value);
                    return;
                }
                if (HigherScope == null)
                    throw new VariableException("Variable " + name + " doesn't exist");
                HigherScope.ModifyVariableValue(name, value);

            }
        }

        #region Get Values
        public string GetLiteralValue(string name)
        {
            lock (_variables)
            {
                if (_variables.ContainsKey(name))
                    return _variables[name];
                if (HigherScope == null)
                    throw new VariableException("Variable " + name + " not set");
                return HigherScope.GetLiteralValue(name);
            }
        }
        public string GetValue(string name)
        {
            return ReplaceVariables(name, GetLiteralValue(name));
        }
        public byte GetByte(string name)
        {
            string value = GetValue(name);

            try
            {
                return Convert.ToByte(value);
            }
            catch
            {
                throw new VariableException("Variable \"" + name + "\" is not a Byte.");
            }
        }
        #endregion

        public List<string> Variables
        {
            get
            {
                return new List<string>(_variables.Keys);
            }
        }

      
        public bool IsValidVariableName(string name)
        {
            var pattern = new Regex(@"\A" + VariableNamePattern + @"\Z");
            return pattern.IsMatch(name);
        }

        public IEnumerable<string> VariablesInString(string input)
        {
            var pattern = new Regex(VariableFormatPattern);
            foreach (Match match in pattern.Matches(input))
            {
                yield return match.Value;
            }
        }

        public string ReplaceVariables(string name, string text)
        {
            var builder = new StringBuilder(text);

            List<string> variables;
            do
            {
                variables = VariablesInString(builder.ToString()).ToList();
                foreach (string variable in variables)
                {
                    var variableName = variable.Replace("${", "").Replace("}", "");
                    string replacement;
                    if (variableName != name)
                    {
                        replacement = GetValue(variableName);
                    }
                    else if (HigherScope != null)
                    {
                        replacement = HigherScope.GetValue(variableName);
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

            return builder.ToString();
        }

        public bool VariableDefinedInThisScope(string name)
        {
            return _variables.ContainsKey(name);
        }
        public bool VariableIsDefined(string name)
        {
            if (VariableDefinedInThisScope(name))
                return true;
            if (HigherScope == null)
                return false;
            return HigherScope.VariableIsDefined(name);
        }

        public void ResetLocalScope()
        {
            _variables.Clear();
        }
    }
}
