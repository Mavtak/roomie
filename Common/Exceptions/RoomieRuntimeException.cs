using System;

namespace Roomie.Common.Exceptions
{
    public class RoomieRuntimeException : Exception
    {
        public RoomieRuntimeException(string message)
            : base(message)
        { }
        public RoomieRuntimeException(string message, Exception innerException = null)
            : base(message, innerException)
        { }
    }
}
