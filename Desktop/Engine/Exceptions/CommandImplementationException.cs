
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
