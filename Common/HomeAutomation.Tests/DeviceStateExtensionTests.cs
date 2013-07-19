using System.Collections.Generic;
using NUnit.Framework;

namespace Roomie.Common.HomeAutomation.Tests
{
    //TODO: make these more thorough, test IDeviceState substates in other test classes
    public class DeviceStateExtensionTests
    {
        public IEnumerable<IDeviceState> Devices
        {
            get
            {
                return DataHelpers.GenerateExampleDevices(20);
            }
        }

        public IEnumerable<IDeviceState> DevicesWithoutToggleSwitches
        {
            get
            {
                return DataHelpers.GenerateExampleDevices(20, false, true, true);
            }
        }

        public IEnumerable<IDeviceState> DevicesWithoutDimmerSwitches
        {
            get
            {
                return DataHelpers.GenerateExampleDevices(20, true, false, true);
            }
        }

        public IEnumerable<IDeviceState> DevicesWithoutThermostats
        {
            get
            {
                return DataHelpers.GenerateExampleDevices(20, true, true, false);
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

        [TestCaseSource("DevicesWithoutToggleSwitches")]
        public void SerializationWorksWithNullToggleSwitch(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy);
        }

        [TestCaseSource("DevicesWithoutDimmerSwitches")]
        public void SerializationWorksWithNullDimmerSwitch(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy);
        }

        [TestCaseSource("DevicesWithoutThermostats")]
        public void SerializationWorksWithNullThermostat(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy);
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
