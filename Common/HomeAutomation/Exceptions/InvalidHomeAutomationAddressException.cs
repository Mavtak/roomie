using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roomie.Common.HomeAutomation.Exceptions
{
    public class InvalidHomeAutomationAddressException : HomeAutomationException
    {
        public InvalidHomeAutomationAddressException(string address, Exception innerException = null)
            : base("Invalid Network address \"" + address + "\".  Network addresses are in the form \"NetworkName/LocationName: DeviceName\", but can be simplified the device name if it is unique.")
        { }
    }
}
