using System;
using Roomie.Common.Exceptions;

namespace Roomie.Desktop.Engine.Exceptions
{
    [Serializable]
    public class CommandImplementationException : RoomieRuntimeException
    {
        public CommandImplementationException(RoomieCommand command, string message)
            : base(command.FullName + " " + message)
        { }
    }
}
