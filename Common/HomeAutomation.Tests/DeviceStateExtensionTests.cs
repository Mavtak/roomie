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
                return DataHelpers.GenerateExampleDevices(20,
                    includeCurrentAction: true,
                    includeToggle: true,
                    includeDimmer: true,
                    includeBinarySensor: true,
                    includePowerSensor: true,
                    includeTemperatureSensor: true,
                    includeHumiditySensor: true,
                    inclueIlluminanceSensor: true,
                    includeThermostat: true,
                    includeKeypad: true
                    );
            }
        }

        public IEnumerable<IDeviceState> DevicesWithoutCurrentAction
        {
            get
            {
                return DataHelpers.GenerateExampleDevices(20,
                    includeCurrentAction: false,
                    includeToggle: true,
                    includeDimmer: true,
                    includeBinarySensor: true,
                    includePowerSensor: true,
                    includeTemperatureSensor: true,
                    includeHumiditySensor: true,
                    inclueIlluminanceSensor: true,
                    includeThermostat: true,
                    includeKeypad: false
                    );
            }
        }

        public IEnumerable<IDeviceState> DevicesWithoutToggleSwitches
        {
            get
            {
                return DataHelpers.GenerateExampleDevices(20,
                    includeCurrentAction: true,
                    includeToggle: false,
                    includeDimmer: true,
                    includeBinarySensor: true,
                    includePowerSensor: true,
                    includeTemperatureSensor: true,
                    includeHumiditySensor: true,
                    inclueIlluminanceSensor: true,
                    includeThermostat: true,
                    includeKeypad: false
                    );
            }
        }

        public IEnumerable<IDeviceState> DevicesWithoutDimmerSwitches
        {
            get
            {
                return DataHelpers.GenerateExampleDevices(20,
                    includeCurrentAction: true,
                    includeToggle: true,
                    includeDimmer: false,
                    includeBinarySensor: true,
                    includePowerSensor: true,
                    includeTemperatureSensor: true,
                    includeHumiditySensor: true,
                    inclueIlluminanceSensor: true,
                    includeThermostat: true,
                    includeKeypad: false
                    );
            }
        }

        public IEnumerable<IDeviceState> DevicesWithoutBinarySensors
        {
            get
            {
                return DataHelpers.GenerateExampleDevices(20,
                    includeCurrentAction: true,
                    includeToggle: true,
                    includeDimmer: true,
                    includeBinarySensor: false,
                    includePowerSensor: true,
                    includeTemperatureSensor: true,
                    includeHumiditySensor: true,
                    inclueIlluminanceSensor: true,
                    includeThermostat: true,
                    includeKeypad: false
                    );
            }
        }

        public IEnumerable<IDeviceState> DevicesWithoutPowerSensors
        {
            get
            {
                return DataHelpers.GenerateExampleDevices(20,
                    includeCurrentAction: true,
                    includeToggle: true,
                    includeDimmer: true,
                    includeBinarySensor: true,
                    includePowerSensor: false,
                    includeTemperatureSensor: true,
                    includeHumiditySensor: true,
                    inclueIlluminanceSensor: true,
                    includeThermostat: true,
                    includeKeypad: true
                    );
            }
        }

        public IEnumerable<IDeviceState> DevicesWithoutTemperatureSensors
        {
            get
            {
                return DataHelpers.GenerateExampleDevices(20,
                    includeCurrentAction: true,
                    includeToggle: true,
                    includeDimmer: true,
                    includeBinarySensor: true,
                    includePowerSensor: true,
                    includeTemperatureSensor: false,
                    includeHumiditySensor: true,
                    inclueIlluminanceSensor: true,
                    includeThermostat: true,
                    includeKeypad: true
                    );
            }
        }

        public IEnumerable<IDeviceState> DevicesWithoutHumiditySensors
        {
            get
            {
                return DataHelpers.GenerateExampleDevices(20,
                    includeCurrentAction: true,
                    includeToggle: true,
                    includeDimmer: true,
                    includeBinarySensor: true,
                    includePowerSensor: true,
                    includeTemperatureSensor: true,
                    includeHumiditySensor: false,
                    inclueIlluminanceSensor: true,
                    includeThermostat: true,
                    includeKeypad: true
                    );
            }
        }

        public IEnumerable<IDeviceState> DevicesWithoutIlluminanceSensors
        {
            get
            {
                return DataHelpers.GenerateExampleDevices(20,
                    includeCurrentAction: true,
                    includeToggle: true,
                    includeDimmer: true,
                    includeBinarySensor: true,
                    includePowerSensor: true,
                    includeTemperatureSensor: true,
                    includeHumiditySensor: false,
                    inclueIlluminanceSensor: true,
                    includeThermostat: true,
                    includeKeypad: true
                    );
            }
        }

        public IEnumerable<IDeviceState> DevicesWithoutThermostats
        {
            get
            {
                return DataHelpers.GenerateExampleDevices(20,
                    includeCurrentAction: true,
                    includeToggle: true,
                    includeDimmer: true,
                    includeBinarySensor: true,
                    includePowerSensor: true,
                    includeTemperatureSensor: true,
                    includeHumiditySensor: true,
                    inclueIlluminanceSensor: true,
                    includeThermostat: false,
                    includeKeypad: false
                    );
            }
        }

        public IEnumerable<IDeviceState> DevicesWithoutKeypads
        {
            get
            {
                return DataHelpers.GenerateExampleDevices(20,
                    includeCurrentAction: true,
                    includeToggle: true,
                    includeDimmer: true,
                    includeBinarySensor: true,
                    includePowerSensor: true,
                    includeTemperatureSensor: true,
                    includeHumiditySensor: true,
                    inclueIlluminanceSensor: true,
                    includeThermostat: true,
                    includeKeypad: false
                    );
            }
        }

        [TestCaseSource("Devices")]
        public void CopyWorks(IDeviceState device)
        {
            var copy = device.Copy();

            //TODO: assert truly separate objects
            AssertionHelpers.AssertDevicesEqual(device, copy,
                checkCurrentAction: true,
                checkToggleSwitch: true,
                checkDimmerSwitch: true,
                checkBinarySensor: true,
                checkPowerSensor: true,
                checkTempeartureSensor: true,
                checkHumiditySensor: true,
                checkIlluminanceSensor: true,
                checkThermostat: true,
                checkKeypad: true
                );
        }

        [TestCaseSource("Devices")]
        public void CurrentActionSerializationWorks(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy,
                checkCurrentAction: true,
                checkToggleSwitch: false,
                checkDimmerSwitch: false,
                checkBinarySensor: false,
                checkPowerSensor: false,
                checkTempeartureSensor: false,
                checkHumiditySensor: false,
                checkIlluminanceSensor: true,
                checkThermostat: false,
                checkKeypad: false
                );
        }

        [TestCaseSource("Devices")]
        public void ToggleSwitchSerializationWorks(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy,
                checkCurrentAction: false,
                checkToggleSwitch: true,
                checkDimmerSwitch: false,
                checkBinarySensor: false,
                checkPowerSensor: false,
                checkTempeartureSensor: false,
                checkHumiditySensor: false,
                checkIlluminanceSensor: true,
                checkThermostat: false,
                checkKeypad: false
                );
        }

        [TestCaseSource("Devices")]
        public void DimmerSwitchSerializationWorks(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy,
                checkCurrentAction: false,
                checkToggleSwitch: false,
                checkDimmerSwitch: true,
                checkBinarySensor: false,
                checkPowerSensor: false,
                checkTempeartureSensor: false,
                checkHumiditySensor: false,
                checkIlluminanceSensor: false,
                checkThermostat: false,
                checkKeypad: false
                );
        }

        [TestCaseSource("Devices")]
        public void BinarySensorSerializationWorks(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy,
                checkCurrentAction: false,
                checkToggleSwitch: false,
                checkDimmerSwitch: false,
                checkBinarySensor: true,
                checkPowerSensor: false,
                checkTempeartureSensor: false,
                checkHumiditySensor: false,
                checkIlluminanceSensor: false,
                checkThermostat: false,
                checkKeypad: false
                );
        }

        [TestCaseSource("Devices")]
        public void PowerSensorSerializationWorks(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy,
                checkCurrentAction: false,
                checkToggleSwitch: false,
                checkDimmerSwitch: false,
                checkBinarySensor: false,
                checkPowerSensor: true,
                checkTempeartureSensor: false,
                checkHumiditySensor: false,
                checkIlluminanceSensor: false,
                checkThermostat: false,
                checkKeypad: false
                );
        }

        public void TemperatureSensorSerializationWorks(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy,
                checkCurrentAction: false,
                checkToggleSwitch: false,
                checkDimmerSwitch: false,
                checkBinarySensor: false,
                checkPowerSensor: false,
                checkTempeartureSensor: true,
                checkHumiditySensor: false,
                checkIlluminanceSensor: false,
                checkThermostat: false,
                checkKeypad: false
                );
        }

        [TestCaseSource("Devices")]
        public void HumiditySensorSerializationWorks(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy,
                checkCurrentAction: false,
                checkToggleSwitch: false,
                checkDimmerSwitch: false,
                checkBinarySensor: false,
                checkPowerSensor: false,
                checkTempeartureSensor: false,
                checkHumiditySensor: true,
                checkIlluminanceSensor: false,
                checkThermostat: false,
                checkKeypad: false
                );
        }

        [TestCaseSource("Devices")]
        public void IlluminanceSensorSerializationWorks(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy,
                checkCurrentAction: false,
                checkToggleSwitch: false,
                checkDimmerSwitch: false,
                checkBinarySensor: false,
                checkPowerSensor: false,
                checkTempeartureSensor: false,
                checkHumiditySensor: false,
                checkIlluminanceSensor: true,
                checkThermostat: false,
                checkKeypad: false
                );
        }

        [TestCaseSource("Devices")]
        public void ThermostatSerializationWorks(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy,
                checkCurrentAction: false,
                checkToggleSwitch: false,
                checkDimmerSwitch: false,
                checkBinarySensor: false,
                checkPowerSensor: false,
                checkTempeartureSensor: false,
                checkHumiditySensor: false,
                checkIlluminanceSensor: false,
                checkThermostat: true,
                checkKeypad: false
                );
        }

        [TestCaseSource("Devices")]
        public void KeypadSerializationWorks(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy,
                checkCurrentAction: false,
                checkToggleSwitch: false,
                checkDimmerSwitch: false,
                checkBinarySensor: false,
                checkPowerSensor: false,
                checkTempeartureSensor: false,
                checkHumiditySensor: false,
                checkIlluminanceSensor: false,
                checkThermostat: false,
                checkKeypad: true
                );
        }

        [TestCaseSource("DevicesWithoutCurrentAction")]
        public void SerializationWorksWithNullCurrentAction(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy,
                checkCurrentAction: false,
                checkToggleSwitch: true,
                checkDimmerSwitch: true,
                checkBinarySensor: true,
                checkPowerSensor: true,
                checkTempeartureSensor: true,
                checkHumiditySensor: true,
                checkIlluminanceSensor: true,
                checkThermostat: true,
                checkKeypad: true
                );
        }

        [TestCaseSource("DevicesWithoutToggleSwitches")]
        public void SerializationWorksWithNullToggleSwitch(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy,
                checkCurrentAction: false,
                checkToggleSwitch: true,
                checkDimmerSwitch: true,
                checkBinarySensor: true,
                checkPowerSensor: true,
                checkTempeartureSensor: true,
                checkHumiditySensor: true,
                checkIlluminanceSensor: true,
                checkThermostat: true,
                checkKeypad: true
                );
        }

        [TestCaseSource("DevicesWithoutDimmerSwitches")]
        public void SerializationWorksWithNullDimmerSwitch(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy,
                checkCurrentAction: false,
                checkToggleSwitch: true,
                checkDimmerSwitch: true,
                checkBinarySensor: true,
                checkPowerSensor: true,
                checkTempeartureSensor: true,
                checkHumiditySensor: true,
                checkIlluminanceSensor: true,
                checkThermostat: true,
                checkKeypad: true
                );
        }

        [TestCaseSource("DevicesWithoutBinarySensors")]
        public void SerializationWorksWithNullBinarySensor(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy,
                checkCurrentAction: false,
                checkToggleSwitch: true,
                checkDimmerSwitch: true,
                checkBinarySensor: true,
                checkPowerSensor: true,
                checkTempeartureSensor: true,
                checkHumiditySensor: true,
                checkIlluminanceSensor: true,
                checkThermostat: true,
                checkKeypad: true
                );
        }

        [TestCaseSource("DevicesWithoutPowerSensors")]
        public void SerializationWorksWithNullPowerSensor(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy,
                checkCurrentAction: false,
                checkToggleSwitch: true,
                checkDimmerSwitch: true,
                checkBinarySensor: true,
                checkPowerSensor: true,
                checkTempeartureSensor: true,
                checkHumiditySensor: true,
                checkIlluminanceSensor: true,
                checkThermostat: true,
                checkKeypad: true
                );
        }

        [TestCaseSource("DevicesWithoutTemperatureSensors")]
        public void SerializationWorksWithNullTemperatureSensor(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy,
                checkCurrentAction: false,
                checkToggleSwitch: true,
                checkDimmerSwitch: true,
                checkBinarySensor: true,
                checkPowerSensor: true,
                checkTempeartureSensor: true,
                checkHumiditySensor: true,
                checkIlluminanceSensor: true,
                checkThermostat: true,
                checkKeypad: true
                );
        }

        [TestCaseSource("DevicesWithoutHumiditySensors")]
        public void SerializationWorksWithNullHumiditySensor(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy,
                checkCurrentAction: false,
                checkToggleSwitch: true,
                checkDimmerSwitch: true,
                checkBinarySensor: true,
                checkPowerSensor: true,
                checkTempeartureSensor: true,
                checkHumiditySensor: true,
                checkIlluminanceSensor: true,
                checkThermostat: true,
                checkKeypad: true
                );
        }

        [TestCaseSource("DevicesWithoutIlluminanceSensors")]
        public void SerializationWorksWithNullIlluminanceSensor(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy,
                checkCurrentAction: false,
                checkToggleSwitch: true,
                checkDimmerSwitch: true,
                checkBinarySensor: true,
                checkPowerSensor: true,
                checkTempeartureSensor: true,
                checkHumiditySensor: true,
                checkIlluminanceSensor: true,
                checkThermostat: true,
                checkKeypad: true
                );
        }

        [TestCaseSource("DevicesWithoutThermostats")]
        public void SerializationWorksWithNullThermostat(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy,
                checkCurrentAction: false,
                checkToggleSwitch: true,
                checkDimmerSwitch: true,
                checkBinarySensor: true,
                checkPowerSensor: true,
                checkTempeartureSensor: true,
                checkHumiditySensor: true,
                checkIlluminanceSensor: true,
                checkThermostat: true,
                checkKeypad: true
                );
        }

        [TestCaseSource("DevicesWithoutKeypads")]
        public void SerializationWorksWithNullKeypad(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy,
                checkCurrentAction: false,
                checkToggleSwitch: true,
                checkDimmerSwitch: true,
                checkBinarySensor: true,
                checkPowerSensor: true,
                checkTempeartureSensor: true,
                checkHumiditySensor: true,
                checkIlluminanceSensor: true,
                checkThermostat: true,
                checkKeypad: true
                );
        }

        [TestCaseSource("Devices")]
        public void SerializationWorks(IDeviceState device)
        {
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy,
                checkCurrentAction: true,
                checkToggleSwitch: true,
                checkDimmerSwitch: true,
                checkBinarySensor: true,
                checkPowerSensor: true,
                checkTempeartureSensor: true,
                checkHumiditySensor: true,
                checkIlluminanceSensor: true,
                checkThermostat: true,
                checkKeypad: true
                );
        }
    }
}
