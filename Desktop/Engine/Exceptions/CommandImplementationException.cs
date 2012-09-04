using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Roomie.Common.Exceptions;

namespace Roomie.Desktop.Engine.Exceptions
{
    public class CommandImplementationException : RoomieRuntimeException
    {
        public CommandImplementationException(RoomieCommand command, string message)
            : base(command.FullName + " " + message)
        { }
    }
}
