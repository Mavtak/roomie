using System;
using System.Text;

namespace Roomie.Common.HomeAutomation.Exceptions
{
    [Serializable]
    public class NoMatchingDeviceException : HomeAutomationException
    {
        public NoMatchingDeviceException(string address, Exception innerException = null)
            : base(buildMessage(address), innerException)
        {
            Data.Add("Address", address);
        }

        private static string buildMessage(string address)
        {
            StringBuilder result = new StringBuilder();

            result.Append("There is no device represented by \"");
            result.Append(address);
            result.Append("\"");

            return result.ToString();
        }
    }
}
