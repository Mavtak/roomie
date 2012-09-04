using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Roomie.Common.Exceptions;

namespace Roomie.Desktop.Engine.Exceptions
{
    class MistypedArgumentException : RoomieRuntimeException
    {
        //TODO
        List<string> mistypedVariables;

        public MistypedArgumentException(List<string> mistypedVariables)
            : base("mistyped variables: " + Common.NiceList(mistypedVariables))
        {
            this.mistypedVariables = mistypedVariables;
        }
    }
}
