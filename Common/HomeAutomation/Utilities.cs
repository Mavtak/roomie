using System;
using System.Text;

using System.Text.RegularExpressions;
using Roomie.Common.HomeAutomation.Exceptions;

namespace Roomie.Common.HomeAutomation
{
    public static class Utilities
    {

        #region name
        internal const string namePattern = @"[a-z0-9][-a-z0-9._ ']*[-a-z0-9._]*";

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


        public static bool ParseAddress(string address, out string networkLocation, out string networkName, out string networkId, out string locationName, out string deviceName, out string deviceId)
        {
            var virtualAddress = VirtualAddress.Parse(address);
            var result = true;

            if (virtualAddress == null)
            {
                virtualAddress = new VirtualAddress();
                result = false;
            }

            networkLocation = virtualAddress.NetworkLocation;
            networkName = virtualAddress.NetworkName;
            networkId = virtualAddress.NetworkNodeId;
            locationName = virtualAddress.DeviceLocation;
            deviceName = virtualAddress.DeviceName;
            deviceId = virtualAddress.DeviceNodeId;

            return result;
        }

        public static string BuildAddress(string networkLocation, string networkAddress, string networkName, string deviceLocation, string deviceAddress, string deviceName, string remarks)
        {
            var virtualAddress = new VirtualAddress
            {
                NetworkNodeId = networkAddress,
                NetworkName = networkName,
                NetworkLocation = networkLocation,
                DeviceNodeId = deviceAddress,
                DeviceName = deviceName,
                DeviceLocation = deviceLocation,
                Remark = remarks
            };

            return virtualAddress.Format();
        }
        #endregion

        public static bool IsOn(int? power)
        {
            return power != null && !IsOff(power);
        }

        public static bool IsOff(int? power)
        {

            return power == 0; ;
        }

        public static int ValidatePower(int power, int? maxPower)
        {
            if (power < 0)
            {
                throw new HomeAutomationException("Power must be greater than or equal to 0 (attempted value is " + power + ")");
            }

            if (power > maxPower)
            {
                power = maxPower.Value;
            }

            return power;
        }
    }
}
