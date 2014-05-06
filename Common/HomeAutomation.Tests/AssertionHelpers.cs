using System.Linq;
using NUnit.Framework;
using Roomie.Common.HomeAutomation.BinarySensors;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.MultilevelSensors;
using Roomie.Common.HomeAutomation.MultilevelSwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Cores;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Measurements;
using Roomie.Common.Measurements.Temperature;

namespace Roomie.Common.HomeAutomation.Tests
{
    public class AssertionHelpers
    {
        public static void AssertDevicesEqual(IDeviceState one, IDeviceState two, bool checkToggleSwitch, bool checkDimmerSwitch, bool checkBinarySensor, bool checkPowerSensor, bool checkTempeartureSensor, bool checkHumiditySensor, bool checkThermostat, bool checkKeypad)
        {
            Assert.That(one.Name, Is.EqualTo(two.Name));
            Assert.That(one.Address, Is.EqualTo(two.Address));
            Assert.That(one.Location, Is.EqualTo(two.Location));
            Assert.That(one.Type, Is.EqualTo(two.Type));

            if (checkToggleSwitch)
            {
                AssertToggleSwitchEqual(one.BinarySwitchState, two.BinarySwitchState);
            }

            if (checkDimmerSwitch)
            {
                AssertDimmerSwitchEqual(one.MultilevelSwitchState, two.MultilevelSwitchState);
            }

            if (checkBinarySensor)
            {
                AssertBinarySensorEqual(one.BinarySensorState, two.BinarySensorState);
            }

            if (checkPowerSensor)
            {
                AssertMultilevelSensorEqual(one.PowerSensorState, two.PowerSensorState);
            }

            if (checkTempeartureSensor)
            {
                AssertMultilevelSensorEqual(one.TemperatureSensorState, two.TemperatureSensorState);
            }

            if (checkHumiditySensor)
            {
                AssertMultilevelSensorEqual(one.HumiditySensorState, two.HumiditySensorState);
            }

            if (checkThermostat)
            {
                AssertThermostatEqual(one.ThermostatState, two.ThermostatState);
            }

            if (checkKeypad)
            {
                AssertKeypadEqual(one.KeypadState, two.KeypadState);
            }
        }

        public static void AssertHelperHelper(object one, object two)
        {
            if (one == two)
            {
                return;
            }

            if (one == null || two == null)
            {
                Assert.That(one, Is.EqualTo(two));
            }
        }

        public static void AssertToggleSwitchEqual(IBinarySwitchState one, IBinarySwitchState two)
        {
            if (one == null && two == null)
            {
                return;
            }

            AssertHelperHelper(one, two);

            Assert.That(one.Power, Is.EqualTo(two.Power));
        }

        public static void AssertDimmerSwitchEqual(IMultilevelSwitchState one, IMultilevelSwitchState two)
        {
            if (one == null && two == null)
            {
                return;
            }

            AssertHelperHelper(one, two);

            Assert.That(one.Power, Is.EqualTo(two.Power));
        }

        public static void AssertBinarySensorEqual(IBinarySensorState one, IBinarySensorState two)
        {
            if (one == null && two == null)
            {
                return;
            }

            AssertHelperHelper(one, two);

            Assert.That(one.Type, Is.EqualTo(two.Type));
            Assert.That(one.Value, Is.EqualTo(two.Value));
        }

        public static void AssertMultilevelSensorEqual<TMeasurement>(IMultilevelSensorState<TMeasurement> one, IMultilevelSensorState<TMeasurement> two)
            where TMeasurement : IMeasurement
        {
            if (one == null && two == null)
            {
                return;
            }

            AssertHelperHelper(one, two);

            Assert.That(one.Value.Format(), Is.EqualTo(two.Value.Format()));
            Assert.That(one.TimeStamp, Is.EqualTo(two.TimeStamp));
        }

        public static void AssertThermostatEqual(IThermostatState one, IThermostatState two)
        {
            if (one == null && two == null)
            {
                return;
            }

            AssertHelperHelper(one, two);

            AssertTemperatureEqual(one.Temperature, two.Temperature);
            AssertThermostatCoreEqual(one.CoreState, two.CoreState);
            AssertThermostatFanEqual(one.FanState, two.FanState);
            AssertThermostatSetpointsAreEqual(one.SetpointStates, two.SetpointStates);
        }

        public static void AssertTemperatureEqual(ITemperature one, ITemperature two)
        {
            if (one == null && two == null)
            {
                return;
            }

            AssertHelperHelper(one, two);

            Assert.That(one.GetType(), Is.EqualTo(two.GetType()));
            Assert.That(one.Value, Is.EqualTo(two.Value));
        }

        public static void AssertThermostatCoreEqual(IThermostatCoreState one, IThermostatCoreState two)
        {
            if (one == null && two == null)
            {
                return;
            }

            AssertHelperHelper(one, two);

            Assert.That(one.Mode, Is.EqualTo(two.Mode));
            CollectionAssert.AreEquivalent(one.SupportedModes, two.SupportedModes);
            Assert.That(one.CurrentAction, Is.EqualTo(two.CurrentAction));
        }

        public static void AssertThermostatFanEqual(IThermostatFanState one, IThermostatFanState two)
        {
            if (one == null && two == null)
            {
                return;
            }

            AssertHelperHelper(one, two);

            Assert.That(one.Mode, Is.EqualTo(two.Mode));
            CollectionAssert.AreEquivalent(one.SupportedModes, two.SupportedModes);
            Assert.That(one.CurrentAction, Is.EqualTo(two.CurrentAction));
        }

        public static void AssertThermostatSetpointsAreEqual(IThermostatSetpointCollectionState one, IThermostatSetpointCollectionState two)
        {
            if (one == null && two == null)
            {
                return;
            }

            AssertHelperHelper(one, two);

            CollectionAssert.AreEquivalent(one.AvailableSetpoints, two.AvailableSetpoints);

            foreach (var setpoint in one.AvailableSetpoints)
            {
                CollectionAssert.Contains(two.AvailableSetpoints, setpoint);
                Assert.That(one[setpoint], Is.EqualTo(two[setpoint]));
            }
        }

        public static void AssertKeypadEqual(IKeypadState one, IKeypadState two)
        {
            if (one == null && two == null)
            {
                return;
            }

            AssertHelperHelper(one, two);

            Assert.That(one.Buttons.Count(), Is.EqualTo(two.Buttons.Count()));

            foreach (var buttonOne in one.Buttons)
            {
                var buttonTwo = two.Buttons.FirstOrDefault(x => x.Id == buttonOne.Id);
                Assert.That(buttonTwo, Is.Not.Null);
                Assert.That(buttonOne.Pressed, Is.EqualTo(buttonTwo.Pressed));
            }
        }
    }
}
