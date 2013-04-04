using System;
using System.Text;
using Roomie.Common.HomeAutomation.Exceptions;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Exceptions
{
    public class DeviceNotRespondingException : HomeAutomationException
    {
        public Device Device { get; private set; }

        public DeviceNotRespondingException(Device device, Exception innerException = null)
            : base(GenerateMessage(device), innerException)
        {
            Device = device;
        }

        private static string GenerateMessage(Device device)
        {
            var result = new StringBuilder();
            result.Append("Device \"");
            result.Append(device);
            result.Append("\" is not responding.");

            return result.ToString();
        }
    }
}
