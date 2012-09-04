using System;
using System.Collections.Generic;
using System.Text;

using System.Text.RegularExpressions;

using Roomie.Desktop.Engine.Exceptions;

namespace Roomie.Desktop.Engine
{
    public class RoomieCommandScope
    {
        public const string VariableNamePattern = "[A-Za-z0-9-_ ]+?";
        public const string VariableFormatPattern = @"\$\{" + VariableNamePattern + @"\}";

        private Dictionary<string, string> variables;
        private RoomieCommandScope higherScope;

        public RoomieCommandScope(RoomieCommandScope higherScope)
        {
            this.higherScope = higherScope;
            variables = new Dictionary<string, string>();
        }
        public RoomieCommandScope()
            : this(null)
        {
        }

        public RoomieCommandScope HigherScope
        {
            get
            {
                return higherScope;
            }
        }

        public ICollection<string> Names
        {
            get
            {
                return variables.Keys;
            }
        }

        public RoomieCommandScope CreateLowerScope()
        {
            return new RoomieCommandScope(this);
        }

        public bool ContainsVariableInScope(string name)
        {
            return variables.ContainsKey(name);
        }

        public void DeclareVariable(string name, string value)
        {
            if (!isValidVariableName(name))
                throw new VariableException("\"" + name + "\" is not a valid variable name");

            lock (variables)
            {
                if (ContainsVariableInScope(name))
                    throw new VariableException("Variable \"" + name + "\" already exists.");
                variables.Add(name, value);
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
            lock (variables)
            {
                if (variables.ContainsKey(name))
                {
                    variables.Remove(name);
                    variables.Add(name, value);
                    return;
                }
                if (higherScope == null)
                    throw new VariableException("Variable " + name + " doesn't exist");
                higherScope.ModifyVariableValue(name, value);

            }
        }

        #region Get Values
        public string GetLiteralValue(string name)
        {
            lock (variables)
            {
                if (variables.ContainsKey(name))
                    return variables[name];
                if (higherScope == null)
                    throw new VariableException("Variable " + name + " not set");
                return higherScope.GetLiteralValue(name);
            }
        }
        public string GetValue(string name)
        {
            return ReplaceVariables(GetLiteralValue(name));
        }
        public bool GetBoolean(string name)
        {
            string value = GetValue(name);

            try
            {
                return Convert.ToBoolean(value);
            }
            catch
            {
                throw new VariableException("Variable \"" + name + "\" is not a Boolean.");
            }
        }
        public int GetInteger(string name)
        {
            string value = GetValue(name);

            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                throw new VariableException("Variable \"" + name + "\" is not an Integer.");
            }
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
        public TimeSpan GetTimeSpan(string name)
        {
            return TimeUtils.StringToTimeSpan(GetValue(name));
        }
        public DateTime GetDateTime(string name)
        {
            return TimeUtils.StringToDateTime(GetValue(name));
        }
        #endregion

        public List<string> Variables
        {
            get
            {
                return new List<string>(variables.Keys);
            }
        }

      
        public bool isValidVariableName(string name)
        {
            Regex pattern = new Regex(@"\A" + VariableNamePattern + @"\Z");
            return pattern.IsMatch(name);
        }

        public List<string> VariablesInString(string input)
        {
            List<string> result = new List<string>();

            Regex pattern = new Regex(VariableFormatPattern);
            foreach (Match match in pattern.Matches(input))
                result.Add(match.Value);

            return result;
        }

        public string ReplaceVariables(string text)
        {
            var builder = new StringBuilder(text);

            List<string> variables;
            do
            {
                variables = VariablesInString(builder.ToString());
                foreach (string variable in variables)
                {
                    var replacement = GetValue(variable.Replace("${", "").Replace("}", ""));
                    if (replacement == null)
                    {
                        throw new VariableException("Variable \"" + variable + "\" not defined");
                    }
                    builder.Replace(variable, replacement);//TODO
                }
            } while (variables.Count != 0);

            return builder.ToString();
        }

        public bool VariableDefinedInThisScope(string name)
        {
            return variables.ContainsKey(name);
        }
        public bool VariableIsDefined(string name)
        {
            if (VariableDefinedInThisScope(name))
                return true;
            if (higherScope == null)
                return false;
            return higherScope.VariableIsDefined(name);
        }

        public void ResetLocalScope()
        {
            variables.Clear();
        }
    }
}
