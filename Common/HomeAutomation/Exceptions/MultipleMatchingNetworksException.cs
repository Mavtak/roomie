using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roomie.Common.HomeAutomation.Exceptions
{
    [Serializable]
    public class MultipleMatchingNetworksException : HomeAutomationException
    {
        public MultipleMatchingNetworksException(string address, IEnumerable<Network> networks, Exception innerException = null)
            : base(buildMessage(address, networks), innerException)
        {
            //TODO: is this bad to do?  Security?
            this.Data.Add("Address", address);
            this.Data.Add("Networks", networks);
        }

        private static string buildMessage(string address, IEnumerable<Network> networks)
        {
            StringBuilder result = new StringBuilder();

            result.Append("\"");
            result.Append(address);
            result.Append("\"");
            result.Append(" could refer to multiple devices: ");

            if (networks == null || !networks.Any())
            {
                result.Append("(no devices given)");
            }
            else
            {
                result.Append("{");
                //TODO: use TextUtilities
                foreach (var network in networks)
                {
                    result.Append("\"");
                    result.Append(network.Name);
                    result.Append("\"");

                    if (network != networks.Last())
                    {
                        result.Append(", ");
                    }
                }
                result.Append("}");
            }

            return result.ToString();
        }
    }
}
