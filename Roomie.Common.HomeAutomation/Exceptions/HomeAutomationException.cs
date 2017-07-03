using System;
using Roomie.Common.Exceptions;

namespace Roomie.Common.HomeAutomation.Exceptions
{
    [Serializable]
    public class HomeAutomationException : RoomieRuntimeException
    {
        public HomeAutomationException(string message, Exception innerException = null)
            : base(message, innerException)
        { }
    }
}
