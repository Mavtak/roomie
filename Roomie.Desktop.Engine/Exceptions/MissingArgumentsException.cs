using System;
using System.Collections.Generic;
using System.Linq;

namespace Roomie.Desktop.Engine.Exceptions
{
    [Serializable]
    public class MissingArgumentsException : VariableException
    {
        public MissingArgumentsException(IEnumerable<string> missingVariables)
            : base("missing variables: " + Common.NiceList(missingVariables))
        {
        }

        public MissingArgumentsException(IEnumerable<RoomieCommandArgument> missingVariables)
            : this(missingVariables.Select(x => x.Name))
        {
        }

        public MissingArgumentsException(string argument)
            :this(new []{argument})
        {
        }
    }
}
