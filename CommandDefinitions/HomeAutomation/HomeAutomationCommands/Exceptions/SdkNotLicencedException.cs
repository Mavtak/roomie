using System;
using Roomie.Common.HomeAutomation.Exceptions;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Exceptions
{
    [Serializable]
    public class SdkNotLicencedException : HomeAutomationException
    {
        public SdkNotLicencedException()
            : base("The ControlThink SDK isn't licenced.  Plug in your ThinkStick and restart the program.")
        {
        }
    }
}
