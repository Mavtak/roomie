using Roomie.Common.HomeAutomation.Exceptions;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Exceptions
{
    public class FailedToConnectException : HomeAutomationException
    {
        public FailedToConnectException()
            : base("Failed To Connect")
        {
        }
    }
}
