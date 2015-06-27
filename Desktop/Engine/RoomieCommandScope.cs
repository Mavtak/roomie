using System.Collections.Generic;
using System.Text.RegularExpressions;
using Roomie.Desktop.Engine.Exceptions;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.Desktop.Engine
{
    //TODO: reconsider the structure of this class
    public class RoomieCommandScope
    {
        public const string VariableNamePattern = "[A-Za-z0-9-_ ]+?";
        public const string VariableFormatPattern = @"\$\{" + VariableNamePattern + @"\}";

        private readonly Dictionary<string, VariableParameter> _variables;
        public RoomieCommandScope HigherScope { get; private set; }

        public RoomieCommandScope(RoomieCommandScope higherScope)
        {
            HigherScope = higherScope;
            _variables = new Dictionary<string, VariableParameter>();
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
            {
                throw new VariableException("\"" + name + "\" is not a valid variable name");
            }

            lock (_variables)
            {
                if (ContainsVariableInScope(name))
                {
                    throw new VariableException("Variable \"" + name + "\" already exists.");
                }

                _variables.Add(name, new VariableParameter(name, value));
            }
        }

        public void DeclareOrUpdateVariable(string name, string value)
        {
            lock (_variables)
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

        public VariableParameter TryGetVariable(string name)
        {
            lock (_variables)
            {
                if (_variables.ContainsKey(name))
                {
                    return _variables[name];
                }

                if (HigherScope == null)
                {
                    return null;
                }

                return HigherScope.TryGetVariable(name);
            }
        }

        public VariableParameter GetVariable(string name)
        {
            var result = TryGetVariable(name);

            if(result == null)
            {
                throw new VariableException("Variable " + name + " not set");
            }

            return result;
        }

        public IParameter ReadParameter(string name)
        {
            var variable = GetVariable(name);
            var result = variable.Interpolate(this);

            return result;
        }

        public List<string> VariableNames
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

        public bool VariableDefinedInThisScope(string name)
        {
            return _variables.ContainsKey(name);
        }

        public bool VariableIsDefined(string name)
        {
            if (VariableDefinedInThisScope(name))
            {
                return true;
            }

            if (HigherScope == null)
            {
                return false;
            }

            return HigherScope.VariableIsDefined(name);
        }

        public void ResetLocalScope()
        {
            _variables.Clear();
        }
    }
}
