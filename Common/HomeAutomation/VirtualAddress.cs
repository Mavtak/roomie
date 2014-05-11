using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Roomie.Common.HomeAutomation
{
    public class VirtualAddress
    {
        private const string namePattern = Utilities.namePattern;

        private const string addressPatttern = @"^"

            //network part (optional)
            + @"(?: "
            + @"(?: (?<network_location>" + namePattern + ")[:][ ])?"
            + @"  (?<network_name>" + namePattern + ")?"
            + @"[ ]?"
            + @"  ([[] (?<network_id>" + namePattern + ") []])?"
            + @"/)?"

            //location part (optional)
            + @"(?: (?<location_name>" + namePattern + ")[:][ ])?"

            //device name (required)
            + @"(?<device_name>" + namePattern + ")?"
            + @"[ ]?"
            + @"  ([[] (?<device_id>" + namePattern + ")[]])?"

            //remarks (optional)
            + @"("
            + @"[ ]?"
            + @"[#](?<remarks>.*)"
            + ")?"

            + @"$";
        private static Regex addressRegex = new Regex(addressPatttern, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);


        public string NetworkNodeId { get; set; }
        public string NetworkLocation { get; set; }
        public string NetworkName { get; set; }

        public string DeviceNodeId { get; set; }
        public string DeviceLocation { get; set; }
        public string DeviceName { get; set; }

        public string Remark { get; set; }

        public static VirtualAddress Parse(string format)
        {
            if (String.IsNullOrWhiteSpace(format))
            {
                return null;
            }

            var match = addressRegex.Match(format);

            if (!match.Success)
            {
                return null;
            }

            var result = new VirtualAddress
            {
                NetworkNodeId = getValue(match, "network_id"),
                NetworkName = getValue(match, "network_name"),
                NetworkLocation = getValue(match, "network_location"),

                DeviceNodeId = getValue(match, "device_id"),
                DeviceName = getValue(match, "device_name"),
                DeviceLocation = getValue(match, "location_name"),

                //TODO: unit test remarks
                Remark = getValue(match, "remarks")
            };

            return result;
        }

        public static bool IsValid(string format)
        {
            var result = addressRegex.IsMatch(format);

            return result;
        }

        private static string getValue(Match match, string key)
        {
            if (match.Groups[key].Success)
            {
                return match.Groups[key].Value;
            }

            return null;
        }

        public string Format()
        {
            var result = new StringBuilder();

            if (NetworkName != null || NetworkNodeId != null)
            {
                if (NetworkLocation != null)
                {
                    result.Append(NetworkLocation);
                    result.Append(": ");
                }
                if (NetworkName != null)
                {
                    result.Append(NetworkName);
                    result.Append(" ");
                }

                result.Append("[");
                result.Append(NetworkNodeId);
                result.Append("]");

                result.Append("/");
            }


            if (DeviceName != null || DeviceNodeId != null)
            {
                if (DeviceLocation != null)
                {
                    result.Append(DeviceLocation);
                    result.Append(": ");
                }
                if (DeviceName != null)
                {
                    result.Append(DeviceName);
                    result.Append(" ");
                }

                result.Append("[");
                result.Append(DeviceNodeId);
                result.Append("]");
            }

            if (Remark != null)
            {
                result.Append(" # ");
                result.Append(Remark);
            }

            return result.ToString();
        }

        public static string Format(IDeviceState device, bool justAddresses = false, string remarks = null)
        {
            var network = device.NetworkState;

            var virtualAddress = new VirtualAddress
            {
                NetworkNodeId = network.Address,
                NetworkName = (justAddresses) ? (null) : (network.Name),
                //TODO: fix this
                NetworkLocation = null, //(justAddresses) ? (null) : network.Location_Hack,

                DeviceNodeId =  device.Address,
                DeviceName = (justAddresses) ? (null) : (device.Name),
                DeviceLocation = (justAddresses || device.Location == null) ? (null) : (device.Location.Format()),

                Remark = remarks
            };

            return virtualAddress.Format();
        }

        public static string FormatNaturalLanguageDescription(IDeviceState device)
        {
            var result = new StringBuilder();

            if (device.Name != null)
            {
                result.Append("The ");
                result.Append(device.Name);
            }
            else
            {
                result.Append("an unnamed device");
            }

            result.Append(" in ");

            if (device.Location.IsSet)
            {
                // TODO: update this logic to better account for location higherarchy
                result.Append("The ");
                result.Append(device.Location.LastPart());
            }
            else
            {
                result.Append(" an unknown location");
            }

            result.Append(", connected to ");

            if (device.NetworkState != null)
            {
                if (device.NetworkState.Name != null)
                {
                    result.Append("the ");
                    result.Append(device.NetworkState.Name);
                }
                else
                {
                    result.Append("an unnamed network");
                }
            }
            else
            {
                result.Append("no network");
            }

            return result.ToString();
        }
    }
}
