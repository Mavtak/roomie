
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
