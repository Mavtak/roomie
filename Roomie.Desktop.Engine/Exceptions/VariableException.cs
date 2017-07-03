using System;
using Roomie.Common.Exceptions;

namespace Roomie.Desktop.Engine.Exceptions
{
    [Serializable]
    public class VariableException : RoomieRuntimeException
    {
        public VariableException(string message)
            : base(message)
        {
        }
    }
}
