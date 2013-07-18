using System.Collections.Generic;
using NUnit.Framework;

namespace Roomie.Common.HomeAutomation.Tests
{
    public class DeviceStateExtensionTests
    {
        public IEnumerable<IDeviceState> Devices
        {
            get
            {
                return DataHelpers.GenerateExampleDevices(20);
            }
        }

        [TestCaseSource("Devices")]
        public void CopyWorks(IDeviceState device)
        {
            var copy = device.Copy();

            //TODO: assert truly separate objects
            AssertionHelpers.AssertDevicesEqual(device, copy);
        }

        [TestCaseSource("Devices")]
        public void ToggleSwitchSerializationWorks(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy, true, false, false);
        }

        [TestCaseSource("Devices")]
        public void DimmerSwitchSerializationWorks(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy, false, true, false);
        }

        [TestCaseSource("Devices")]
        public void ThermostatSerializationWorks(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy, false, false, true);
        }

        [TestCaseSource("Devices")]
        public void SerializationWorks(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy);
        }
    }
}
