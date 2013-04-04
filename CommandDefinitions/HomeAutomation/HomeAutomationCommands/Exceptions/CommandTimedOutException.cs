using System;
using System.Text;
using Roomie.Common.HomeAutomation.Exceptions;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Exceptions
{
    public class CommandTimedOutException : HomeAutomationException
    {
        public Device Device { get; private set; }

        public CommandTimedOutException(Device device, Exception innerException = null)
            : base(GenerateMessage(device), innerException)
        {
            Device = device;
        }

        private static string GenerateMessage(Device device)
        {
            var result = new StringBuilder();
            result.Append("Command for device \"");
            result.Append(device);
            result.Append("\" timed out.");

            return result.ToString();
        }
    }
}
