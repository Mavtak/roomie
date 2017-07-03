using NUnit.Framework;

namespace Roomie.Common.HomeAutomation.Tests
{
    //TODO: make these more thorough, test IDeviceState substates in other test classes
    public class DeviceStateExtensionTests
    {
        private const bool X = true;
        private const bool O = false;

        [Test]
        public void CopyWorks()
        {
            //TODO: include tests for when properties are null
            var device = DataHelpers.GenerateExampleDevice();
            var copy = device.Copy();

            //TODO: assert truly separate objects
            AssertionHelpers.AssertDevicesEqual(device, copy);
        }

        [TestCase(X, O, O, O, O, O, O, O, O, O, O, TestName = "Without Current Action")]
        [TestCase(O, X, O, O, O, O, O, O, O, O, O, TestName = "Without Binary Switch")]
        [TestCase(O, O, X, O, O, O, O, O, O, O, O, TestName = "Without Multilevel Switch")]
        [TestCase(O, O, O, X, O, O, O, O, O, O, O, TestName = "Without Color Switch")]
        [TestCase(O, O, O, O, X, O, O, O, O, O, O, TestName = "Without Binary Sensor")]
        [TestCase(O, O, O, O, O, X, O, O, O, O, O, TestName = "Without Power Sensor")]
        [TestCase(O, O, O, O, O, O, X, O, O, O, O, TestName = "Without Temperature Sensor")]
        [TestCase(O, O, O, O, O, O, O, X, O, O, O, TestName = "Without Humidity Sensor")]
        [TestCase(O, O, O, O, O, O, O, O, X, O, O, TestName = "Without Illuminance Sensor")]
        [TestCase(O, O, O, O, O, O, O, O, O, X, O, TestName = "Without Thermostat")]
        [TestCase(O, O, O, O, O, O, O, O, O, O, X, TestName = "Without Keypad")]
        [TestCase(X, X, X, X, X, X, X, X, X, X, X, TestName = "Nothing")]
        public void SerializationWorks(bool currentAction, bool binarySwitch, bool multilevelSwitch, bool colorSwitch, bool binarySensor, bool powerSensor, bool tempeartureSensor, bool humiditySensor, bool illuminanceSensor, bool thermostat, bool keypad)
        {
            var device = DataHelpers.GenerateExampleDevice();
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy,
                checkCurrentAction: currentAction,
                checkToggleSwitch: binarySwitch,
                checkDimmerSwitch: multilevelSwitch,
                checkColorSwitch: colorSwitch,
                checkBinarySensor: binarySensor,
                checkPowerSensor: powerSensor,
                checkTempeartureSensor: tempeartureSensor,
                checkHumiditySensor: humiditySensor,
                checkIlluminanceSensor: illuminanceSensor,
                checkThermostat: thermostat,
                checkKeypad: keypad
                );
        }

        [TestCase(X, O, O, O, O, O, O, O, O, O, O, TestName = "Without Current Action")]
        [TestCase(O, X, O, O, O, O, O, O, O, O, O, TestName = "Without Binary Switch")]
        [TestCase(O, O, X, O, O, O, O, O, O, O, O, TestName = "Without Multilevel Switch")]
        [TestCase(O, O, O, X, O, O, O, O, O, O, O, TestName = "Without Color Switch")]
        [TestCase(O, O, O, O, X, O, O, O, O, O, O, TestName = "Without Binary Sensor")]
        [TestCase(O, O, O, O, O, X, O, O, O, O, O, TestName = "Without Power Sensor")]
        [TestCase(O, O, O, O, O, O, X, O, O, O, O, TestName = "Without Temperature Sensor")]
        [TestCase(O, O, O, O, O, O, O, X, O, O, O, TestName = "Without Humidity Sensor")]
        [TestCase(O, O, O, O, O, O, O, O, X, O, O, TestName = "Without Illuminance Sensor")]
        [TestCase(O, O, O, O, O, O, O, O, O, X, O, TestName = "Without Thermostat")]
        [TestCase(O, O, O, O, O, O, O, O, O, O, X, TestName = "Without Keypad")]
        [TestCase(X, X, X, X, X, X, X, X, X, X, X, TestName = "Nothing")]
        public void SerializationWorksWithNullProperty(bool currentAction, bool binarySwitch, bool multilevelSwitch, bool colorSwitch, bool binarySensor, bool powerSensor, bool tempeartureSensor, bool humiditySensor, bool illuminanceSensor, bool thermostat, bool keypad)
        {
            var device = DataHelpers.GenerateExampleDevice(
                type: DeviceType.Controller,
                includeCurrentAction: !currentAction,
                includeToggle: !binarySwitch,
                includeDimmer: !multilevelSwitch,
                includeColorSwitch: !colorSwitch,
                includeBinarySensor: !binarySensor,
                includePowerSensor: !powerSensor,
                includeTemperatureSensor: !tempeartureSensor,
                includeHumiditySensor: !humiditySensor,
                includeIlluminanceSensor: !illuminanceSensor,
                includeThermostat: !thermostat,
                includeKeypad: !keypad
                );
            var node = device.ToXElement();
            var copy = node.ToDeviceState();

            AssertionHelpers.AssertDevicesEqual(device, copy);
        }
    }
}
