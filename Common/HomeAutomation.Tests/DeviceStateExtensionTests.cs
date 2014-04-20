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
                return DataHelpers.GenerateExampleDevices(20, includeToggle: true, includeDimmer: true, includeBinarySensor: true, includePowerSensor: true, includeHumiditySensor: true, includeThermostat: true, includeKeypad: true);
            }
        }

        public IEnumerable<IDeviceState> DevicesWithoutToggleSwitches
        {
            get
            {
                return DataHelpers.GenerateExampleDevices(20, includeToggle: false, includeDimmer: true, includeBinarySensor: true, includePowerSensor: true, includeHumiditySensor: true, includeThermostat: true, includeKeypad: false);
            }
        }

        public IEnumerable<IDeviceState> DevicesWithoutDimmerSwitches
        {
            get
            {
                return DataHelpers.GenerateExampleDevices(20, includeToggle: true, includeDimmer: false, includeBinarySensor: true, includePowerSensor: true, includeHumiditySensor: true, includeThermostat: true, includeKeypad: false);
            }
        }

        public IEnumerable<IDeviceState> DevicesWithoutBinarySensors
        {
            get
            {
                return DataHelpers.GenerateExampleDevices(20, includeToggle: true, includeDimmer: true, includeBinarySensor: false, includePowerSensor: true, includeHumiditySensor: true, includeThermostat: true, includeKeypad: false);
            }
        }

        public IEnumerable<IDeviceState> DevicesWithoutPowerSensors
        {
            get
            {
                return DataHelpers.GenerateExampleDevices(20, includeToggle: true, includeDimmer: true, includeBinarySensor: true, includePowerSensor: false, includeHumiditySensor: true, includeThermostat: true, includeKeypad: true);
            }
        }

        public IEnumerable<IDeviceState> DevicesWithoutHumiditySensors
        {
            get
            {
                return DataHelpers.GenerateExampleDevices(20, includeToggle: true, includeDimmer: true, includeBinarySensor: true, includePowerSensor: true, includeHumiditySensor: false, includeThermostat: true, includeKeypad: true);
            }
        }

        public IEnumerable<IDeviceState> DevicesWithoutThermostats
        {
            get
            {
                return DataHelpers.GenerateExampleDevices(20, includeToggle: true, includeDimmer: true, includeBinarySensor: true, includePowerSensor: true, includeHumiditySensor: true, includeThermostat: false, includeKeypad: false);
            }
        }

        public IEnumerable<IDeviceState> DevicesWithoutKeypads
        {
            get
            {
                return DataHelpers.GenerateExampleDevices(20, includeToggle: true, includeDimmer: true, includeBinarySensor: true, includePowerSensor: true, includeHumiditySensor: true, includeThermostat: true, includeKeypad: false);
            }
        }

        [TestCaseSource("Devices")]
        public void CopyWorks(IDeviceState device)
        {
            var copy = device.Copy();

            //TODO: assert truly separate objects
            AssertionHelpers.AssertDevicesEqual(device, copy, checkToggleSwitch: true, checkDimmerSwitch: true, checkBinarySensor: true, checkPowerSensor: true, checkHumiditySensor: true, checkThermostat: true, checkKeypad: true);
        }

        [TestCaseSource("Devices")]
        public void ToggleSwitchSerializationWorks(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy, checkToggleSwitch: true, checkDimmerSwitch: false, checkBinarySensor: false, checkPowerSensor: false, checkHumiditySensor: false, checkThermostat: false, checkKeypad: false);
        }

        [TestCaseSource("Devices")]
        public void DimmerSwitchSerializationWorks(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy, checkToggleSwitch: false, checkDimmerSwitch: true, checkBinarySensor: false, checkPowerSensor: false, checkHumiditySensor: false, checkThermostat: false, checkKeypad: false);
        }

        [TestCaseSource("Devices")]
        public void BinarySensorSerializationWorks(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy, checkToggleSwitch: false, checkDimmerSwitch: false, checkBinarySensor: true, checkPowerSensor: false, checkHumiditySensor: false, checkThermostat: false, checkKeypad: false);
        }

        [TestCaseSource("Devices")]
        public void PowerSensorSerializationWorks(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy, checkToggleSwitch: false, checkDimmerSwitch: false, checkBinarySensor: false, checkPowerSensor: true, checkHumiditySensor: false, checkThermostat: false, checkKeypad: false);
        }

        [TestCaseSource("Devices")]
        public void HumiditySensorSerializationWorks(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy, checkToggleSwitch: false, checkDimmerSwitch: false, checkBinarySensor: false, checkPowerSensor: false, checkHumiditySensor: true, checkThermostat: false, checkKeypad: false);
        }

        [TestCaseSource("Devices")]
        public void ThermostatSerializationWorks(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy, checkToggleSwitch: false, checkDimmerSwitch: false, checkBinarySensor: false, checkPowerSensor: false, checkHumiditySensor: false, checkThermostat: true, checkKeypad: false);
        }

        [TestCaseSource("Devices")]
        public void KeypadSerializationWorks(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy, checkToggleSwitch: false, checkDimmerSwitch: false, checkBinarySensor: false, checkPowerSensor: false, checkHumiditySensor: false, checkThermostat: false, checkKeypad: true);
        }

        [TestCaseSource("DevicesWithoutToggleSwitches")]
        public void SerializationWorksWithNullToggleSwitch(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy, checkToggleSwitch: true, checkDimmerSwitch: true, checkBinarySensor: true, checkPowerSensor: true, checkHumiditySensor: true, checkThermostat: true, checkKeypad: true);
        }

        [TestCaseSource("DevicesWithoutDimmerSwitches")]
        public void SerializationWorksWithNullDimmerSwitch(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy, checkToggleSwitch: true, checkDimmerSwitch: true, checkBinarySensor: true, checkPowerSensor: true, checkHumiditySensor: true, checkThermostat: true, checkKeypad: true);
        }

        [TestCaseSource("DevicesWithoutBinarySensors")]
        public void SerializationWorksWithNullBinarySensor(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy, checkToggleSwitch: true, checkDimmerSwitch: true, checkBinarySensor: true, checkPowerSensor: true, checkHumiditySensor: true, checkThermostat: true, checkKeypad: true);
        }

        [TestCaseSource("DevicesWithoutPowerSensors")]
        public void SerializationWorksWithNullPowerSensor(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy, checkToggleSwitch: true, checkDimmerSwitch: true, checkBinarySensor: true, checkPowerSensor: true, checkHumiditySensor: true, checkThermostat: true, checkKeypad: true);
        }

        [TestCaseSource("DevicesWithoutHumiditySensors")]
        public void SerializationWorksWithNullHumiditySensor(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy, checkToggleSwitch: true, checkDimmerSwitch: true, checkBinarySensor: true, checkPowerSensor: true, checkHumiditySensor: true, checkThermostat: true, checkKeypad: true);
        }

        [TestCaseSource("DevicesWithoutThermostats")]
        public void SerializationWorksWithNullThermostat(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy, checkToggleSwitch: true, checkDimmerSwitch: true, checkBinarySensor: true, checkPowerSensor: true, checkHumiditySensor: true, checkThermostat: true, checkKeypad: true);
        }

        [TestCaseSource("DevicesWithoutKeypads")]
        public void SerializationWorksWithNullKeypad(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy, checkToggleSwitch: true, checkDimmerSwitch: true, checkBinarySensor: true, checkPowerSensor: true, checkHumiditySensor: true, checkThermostat: true, checkKeypad: true);
        }

        [TestCaseSource("Devices")]
        public void SerializationWorks(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy, checkToggleSwitch: true, checkDimmerSwitch: true, checkBinarySensor: true, checkPowerSensor: true, checkHumiditySensor: true, checkThermostat: true, checkKeypad: true);
        }
    }
}
