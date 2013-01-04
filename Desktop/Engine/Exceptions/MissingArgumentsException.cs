using System;
using System.Collections.Generic;

namespace Roomie.Desktop.Engine.Exceptions
{
    [Serializable]
    public class MissingArgumentsException : VariableException
    {
        public MissingArgumentsException(List<string> missingVariables)
            : base("missing variables: " + Common.NiceList(missingVariables))
        {
        }

        public MissingArgumentsException(string argument)
            :this(new List<string>(new string[]{argument}))
        {
        }
    }
}
