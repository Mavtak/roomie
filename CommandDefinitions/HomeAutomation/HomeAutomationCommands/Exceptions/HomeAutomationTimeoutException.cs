using System;
using Roomie.Common.HomeAutomation.Exceptions;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Exceptions
{
    public class HomeAutomationTimeoutException : HomeAutomationException
    {
        public Device Device { get; private set; }

        public HomeAutomationTimeoutException(Device device, Exception innerException = null)
            : base("Device '" + device + "' not responding", innerException)
        {
            this.Device = device;
        }
    }
}
