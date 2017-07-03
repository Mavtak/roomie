using System;
using System.Collections.Generic;
using System.Linq;
using Roomie.Common.Exceptions;

namespace Roomie.Desktop.Engine.Exceptions
{
    [Serializable]
    class MistypedArgumentException : RoomieRuntimeException
    {
        public MistypedArgumentException(IEnumerable<string> mistypedVariables)
            : base("mistyped variables: " + Common.NiceList(mistypedVariables))
        {
        }

        public MistypedArgumentException(IEnumerable<RoomieCommandArgument> mistypedVariables)
            : this(mistypedVariables.Select(x => x.Name))
        {
        }
    }
}
