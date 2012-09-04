using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roomie.Common.HomeAutomation.Exceptions
{
    public class NoMatchingNetworkException : HomeAutomationException
    {

        public NoMatchingNetworkException(string networkName, Exception innerException = null)
            : base(buildMessage(networkName), innerException)
        {
            this.Data.Add("NetworkName", networkName);
        }

        private static string buildMessage(string networkName)
        {
            throw new NotImplementedException();
        }
    }
}
