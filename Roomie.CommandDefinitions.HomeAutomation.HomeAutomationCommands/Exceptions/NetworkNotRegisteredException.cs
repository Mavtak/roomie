using System;
using Roomie.Common.HomeAutomation.Exceptions;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Exceptions
{
    public class NetworkNotRegisteredException : HomeAutomationException
    {
        public String NetworkName { get; private set; }

        public NetworkNotRegisteredException(string networkName)
            : base("Network '" + networkName + "' not found.")
        {
            this.NetworkName = networkName;
        }
    }
}
