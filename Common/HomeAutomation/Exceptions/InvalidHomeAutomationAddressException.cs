using System;
using System.Text;

namespace Roomie.Common.HomeAutomation.Exceptions
{
    [Serializable]
    public class InvalidHomeAutomationAddressException : HomeAutomationException
    {
        public InvalidHomeAutomationAddressException(string address, Exception innerException = null)
            : base(BuildMessage(address), innerException)
        {
        }

        private static string BuildMessage(string address)
        {
            var builder = new StringBuilder();
            builder.Append("Invalid Network address \"");
            builder.Append(address);
            builder.Append(
                "\".  Network addresses are in the form \"NetworkName/LocationName: DeviceName\", but can be simplified the device name if it is unique.");

            return builder.ToString();
        }
    }
}
