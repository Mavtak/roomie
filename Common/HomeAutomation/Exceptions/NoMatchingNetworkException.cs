using System;

namespace Roomie.Common.HomeAutomation.Exceptions
{
    [Serializable]
    public class NoMatchingNetworkException : HomeAutomationException
    {
        public NoMatchingNetworkException(string networkName, Exception innerException = null)
            : base(buildMessage(networkName), innerException)
        {
            Data.Add("NetworkName", networkName);
        }

        private static string buildMessage(string networkName)
        {
            throw new NotImplementedException();
        }
    }
}
