﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roomie.Common.HomeAutomation.Exceptions
{
    [Serializable]
    public class MultipleMatchingDevicesException : HomeAutomationException
    {
        public MultipleMatchingDevicesException(string address, IEnumerable<IDeviceState> devices, Exception innerException = null)
            : base(buildMessage(address, devices), innerException)
        {
            //TODO: is this bad to do?  Security?
            this.Data.Add("Address", address);
            this.Data.Add("Devices", devices);
        }

        private static string buildMessage(string address, IEnumerable<IDeviceState> devices)
        {
            var result = new StringBuilder();

            result.Append("\"");
            result.Append(address);
            result.Append("\"");
            result.Append(" could refer to multiple devices: ");

            if (devices == null || !devices.Any())
            {
                result.Append("(no devices given)");
            }
            else
            {
                result.Append("{");
                foreach (var device in devices)
                {
                    result.Append("\"");
                    result.Append(device.Name);
                    result.Append("\"");

                    if (device != devices.Last())
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
