using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Roomie.Common.HomeAutomation.Tests
{
    [TestClass]
    public class AddressParsing
    {
        #region tests for valid addresses
        [TestMethod]
        public void validInitialCasing()
        {
            testAddress(
                trueNetworkName: "network",
                trueDeviceName: "device",
                address: "network/device",
                isValid: true
            );

            testAddress(
                trueNetworkName: "Network",
                trueDeviceName: "Device",
                address: "Network/Device",
                isValid: true
            );

            testAddress(
                trueNetworkName: "Network",
                trueDeviceName: "device",
                address: "Network/device",
                isValid: true
            );

            testAddress(
                trueNetworkName: "network",
                trueDeviceName: "Device",
                address: "network/Device",
                isValid: true
            );
        }

        [TestMethod]
        public void validNumbersAndSymbols()
        {
            testAddress(
                trueNetworkName: "Thing12",
                trueDeviceName: "bwOo3434Pdf",
                address: "Thing12/bwOo3434Pdf",
                isValid: true
            );

            testAddress(
                trueDeviceName: "device name",
                address: "device name",
                isValid: true
            );
        }

        [TestMethod]
        public void locationsIncluded()
        {
            testAddress(
                trueNetworkName: "network",
                trueLocationName: "location",
                trueDeviceName: "device",
                address: "network/location: device",
                isValid: true
            );

            testAddress(
                trueLocationName: "location",
                trueDeviceName: "device",
                address: "location: device",
                isValid: true
            );

            //TODO: more test cases
        }

        [TestMethod]
        public void JustIdsIsValid()
        {
            testAddress(
                trueNetworkId: "network id",
                trueDeviceId: "device id",
                address: "[network id]/[device id]",
                isValid: true
            );
        }

        [TestMethod]
        public void MoreValidCombinations()
        {
            testAddress(
                trueNetworkId: "network id",
                trueDeviceName: "device name",
                trueDeviceId: "device id",
                address: "[network id]/device name [device id]",
                isValid: true
            );

            testAddress(
                trueNetworkId: "network id",
                trueLocationName: "device location",
                trueDeviceName: "device name",
                trueDeviceId: "device id",
                address: "[network id]/device location: device name [device id]",
                isValid: true
            );

            testAddress(
                trueNetworkId: "network id",
                trueLocationName: "device location",
                trueDeviceName: "device name",
                trueDeviceId: "device id",
                address: "[network id]/device location: device name [device id]",
                isValid: true
            );
        }

        [TestMethod]
        public void FullyQualifiedAddresses()
        {
            testAddress(
                trueNetworkLocation: "network location",
                trueNetworkName: "network name",
                trueNetworkId: "network id",
                trueLocationName: "device location",
                trueDeviceName: "device name",
                trueDeviceId: "device id",
                address: "network location: network name [network id]/device location: device name [device id]",
                isValid: true
            );
        }
        #endregion

        #region tests for invalid addresses

        [TestMethod]
        public void testNonalphaPrefixes()
        {
            assertInvalidFormat("1network/address");
            assertInvalidFormat("Network/1Address");
            assertInvalidFormat("_network/Address");
            assertInvalidFormat("Network/_address");
            assertInvalidFormat(" network/Address");
            assertInvalidFormat("Network/ address");
            assertInvalidFormat(":network/Address");
            assertInvalidFormat("Network/:address");
        }

        [TestMethod]
        public void testInvalidCharacters()
        {
            assertInvalidFormat("net!work/address");
            assertInvalidFormat("Network/Addre!ss");
            assertInvalidFormat("netw@ork/Address");
            assertInvalidFormat("Network/addre@ss");
            assertInvalidFormat("netw@ork/Address");
            assertInvalidFormat("Ne/twork/address");
            assertInvalidFormat("Network/addr/ess");
            assertInvalidFormat("Net\\work/address");
            assertInvalidFormat("Network/addr\\ess");
        }

        [TestMethod]
        public void testInvalidFormat()
        {
            assertInvalidFormat("a/b/c");
            assertInvalidFormat("a: bleh/b: c");
            assertInvalidFormat("a/b:c");
            assertInvalidFormat("a/b:: c");
        }

        private void assertInvalidFormat(string format)
        {
            testAddress(
                address: format,
                isValid: false
            );
        }

        [TestMethod]
        public void empty()
        {
            assertInvalidFormat("");
        }

        #endregion

        #region helpers
        void testAddress(bool isValid, string address, string trueNetworkLocation = null, string trueNetworkName = null, string trueNetworkId = null, string trueLocationName = null, string trueDeviceName = null, string trueDeviceId = null)
        {
            string parsedNetworkLocation;
            string parsedNetworkName;
            string parsedNeworkId;

            string parsedLocationName;
            string parsedDeviceName;
            string parsedDeviceId;

            //TODO: include network id and device id in testing

            //TODO: improve this test so that I can be more explicit about this.
            if (String.IsNullOrWhiteSpace(trueNetworkName))
            {
                trueNetworkName = null;
            }

            if (address == null)
            {
                address = Utilities.BuildAddress(
                    networkLocation: trueNetworkLocation,
                    networkName: trueNetworkName,
                    networkAddress: trueNetworkId,
                    deviceLocation: trueNetworkLocation,
                    deviceName: trueDeviceName,
                    deviceAddress: trueDeviceId,
                    remarks: null //TODO: test remarks
                );
            }

            bool success = Utilities.ParseAddress(
                address: address,
                networkLocation: out parsedNetworkLocation,
                networkName: out parsedNetworkName,
                networkId: out parsedNeworkId,
                locationName: out parsedLocationName,
                deviceName: out parsedDeviceName,
                deviceId: out parsedDeviceId
            );

            if (!isValid)
            {
                Assert.IsFalse(success, "Parse succeeded for an invalid address: " + address);
                return;
            }

            Assert.IsTrue(success, "Parse failed for valid address: {" + address + "}");

            Assert.AreEqual(trueNetworkLocation, parsedNetworkLocation, "Incorrectly parsed network location");
            Assert.AreEqual(trueNetworkName, parsedNetworkName, "Incorrectly parsed network name");
            Assert.AreEqual(trueNetworkId, parsedNeworkId, "Incorrectly parsed network id");

            Assert.AreEqual(trueLocationName, parsedLocationName, "Incorrectly parsed location name");
            Assert.AreEqual(trueDeviceName, parsedDeviceName, "Incorrectly parsed device name");
            Assert.AreEqual(trueDeviceId, parsedDeviceId, "Incorrectly parsed device id");
        }
        #endregion
    }
}
