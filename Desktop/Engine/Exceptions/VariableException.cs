using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Roomie.Common.Exceptions;

namespace Roomie.Desktop.Engine.Exceptions
{
    public class VariableException : RoomieRuntimeException
    {
        public VariableException(string message)
            : base(message)
        {
        }
    }
}
