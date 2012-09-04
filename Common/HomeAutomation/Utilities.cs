using System;
using System.Text;

using System.Text.RegularExpressions;

namespace Roomie.Common.HomeAutomation
{
    public static class Utilities
    {

        #region name
        private const string namePattern = @"[a-z0-9][a-z0-9._ -]*[a-z0-9._-]*";

        private static Regex nameRegex = new Regex("^" + namePattern + "$", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        public static bool ValidateName(string name)
        {
            return nameRegex.IsMatch(name);
        }
        #endregion

        #region id

        private const string nodePattern = namePattern;

        public static bool ValidateId(string id)
        {
            return ValidateName(id);
        }

        #endregion

        #region address
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

        public static bool ParseAddress(string address, out string networkLocation, out string networkName, out string networkId, out string locationName, out string deviceName, out string deviceId)
        {
            networkLocation = null;
            networkName = null;
            networkId = null;
            locationName = null;
            deviceName = null;
            deviceId = null;

            //TODO: parse remarks

            if (String.IsNullOrWhiteSpace(address))
            {
                return false;
            }

            var Match = addressRegex.Match(address);

            if (!Match.Success)
            {
                return false;
            }

            if (Match.Groups["network_location"].Success)
            {
                networkLocation= Match.Groups["network_location"].Value;
            }

            if (Match.Groups["network_name"].Success)
            {
                networkName = Match.Groups["network_name"].Value;
            }

            if (Match.Groups["network_id"].Success)
            {
                networkId = Match.Groups["network_id"].Value;
            }



            if (Match.Groups["location_name"].Success)
            {
                locationName = Match.Groups["location_name"].Value;
            }

            if (Match.Groups["device_name"].Success)
            {
                deviceName = Match.Groups["device_name"].Value;
            }

            if (Match.Groups["device_id"].Success)
            {
                deviceId = Match.Groups["device_id"].Value;
            }

            return true;
        }


        //TODO: simplify this
        public static string BuildAddress(string networkLocation, string networkAddress, string networkName, string deviceLocation, string deviceAddress, string deviceName, string remarks)
        {
            var result = new StringBuilder();

            if (networkName != null || networkAddress != null)
            {
                if (networkLocation != null)
                {
                    result.Append(networkLocation);
                    result.Append(": ");
                }
                if (networkName != null)
                {
                    result.Append(networkName);
                    result.Append(" ");
                }

                result.Append("[");
                result.Append(networkAddress);
                result.Append("]");

                result.Append("/");
            }


            if (deviceName != null || deviceAddress != null)
            {
                if (deviceLocation != null)
                {
                    result.Append(deviceLocation);
                    result.Append(": ");
                }
                if (deviceName != null)
                {
                    result.Append(deviceName);
                    result.Append(" ");
                }

                result.Append("[");
                result.Append(deviceAddress);
                result.Append("]");
            }

            if (remarks != null)
            {
                result.Append(" # ");
                result.Append(remarks);
            }

            return result.ToString();
        }

        public static string BuildAddress(this Device device, bool justAddresses = false, string remarks = null)
        {
            var network = device.Network_Hack;

            return BuildAddress(
                networkLocation: null,//networkLocation: network.Location_Hack.Name
                networkAddress: network.Address_Hack,
                networkName: (justAddresses)?(null):(network.Name),
                deviceLocation: (justAddresses) ? (null) : (device.Location_Hack.Name),
                deviceAddress: device.Address_Hack,
                deviceName: (justAddresses)?(null):(device.Name),
                remarks: remarks
            );
        }
        #endregion
    }
}
