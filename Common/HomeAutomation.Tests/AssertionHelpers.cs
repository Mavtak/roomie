﻿using NUnit.Framework;
using Roomie.Common.HomeAutomation.DimmerSwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.HomeAutomation.ToggleSwitches;
using Roomie.Common.Temperature;

namespace Roomie.Common.HomeAutomation.Tests
{
    public class AssertionHelpers
    {
        public static void AssertDevicesEqual(IDeviceState one, IDeviceState two, bool checkToggleSwitch = true, bool checkDimmerSwitch = true, bool checkThermostat = true)
        {
            Assert.That(one.Name, Is.EqualTo(two.Name));
            Assert.That(one.Address, Is.EqualTo(two.Address));
            Assert.That(one.Location.Name, Is.EqualTo(two.Location.Name));
            Assert.That(one.Type, Is.EqualTo(two.Type));

            if (checkToggleSwitch)
            {
                AssertToggleSwitchEqual(one.ToggleSwitchState, two.ToggleSwitchState);
            }

            if (checkDimmerSwitch)
            {
                AssertDimmerSwitchEqual(one.DimmerSwitchState, two.DimmerSwitchState);
            }

            if (checkThermostat)
            {
                AssertThermostatEqual(one.ThermostatState, two.ThermostatState);
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

        public static void AssertToggleSwitchEqual(IToggleSwitchState one, IToggleSwitchState two)
        {
            AssertHelperHelper(one, two);

            Assert.That(one.Power, Is.EqualTo(two.Power));
        }

        public static void AssertDimmerSwitchEqual(IDimmerSwitchState one, IDimmerSwitchState two)
        {
            AssertHelperHelper(one, two);

            Assert.That(one.Power, Is.EqualTo(two.Power));
        }

        public static void AssertThermostatEqual(IThermostatState one, IThermostatState two)
        {
            AssertHelperHelper(one, two);

            Assert.That(one.Mode, Is.EqualTo(two.Mode));
            CollectionAssert.AreEquivalent(one.SupportedModes, two.SupportedModes);
            Assert.That(one.CurrentAction, Is.EqualTo(two.CurrentAction));

            AssertTemperatureEqual(one.Temperature, two.Temperature);
            AssertThermostatFanEqual(one.FanState, two.FanState);
            AssertThermostatSetpointsAreEqual(one.SetPointStates, two.SetPointStates);
        }

        public static void AssertTemperatureEqual(ITemperature one, ITemperature two)
        {
            AssertHelperHelper(one, two);

            Assert.That(one.GetType(), Is.EqualTo(two.GetType()));
            Assert.That(one.Value, Is.EqualTo(two.Value));
        }

        public static void AssertThermostatFanEqual(IThermostatFanState one, IThermostatFanState two)
        {
            AssertHelperHelper(one, two);

            Assert.That(one.Mode, Is.EqualTo(two.Mode));
            CollectionAssert.AreEquivalent(one.SupportedModes, two.SupportedModes);
            Assert.That(one.CurrentAction, Is.EqualTo(two.CurrentAction));
        }

        public static void AssertThermostatSetpointsAreEqual(ISetpointCollectionState one, ISetpointCollectionState two)
        {
            AssertHelperHelper(one, two);

            CollectionAssert.AreEquivalent(one.AvailableSetpoints, two.AvailableSetpoints);

            foreach (var setpoint in one.AvailableSetpoints)
            {
                CollectionAssert.Contains(two.AvailableSetpoints, setpoint);
                Assert.That(one[setpoint], Is.EqualTo(two[setpoint]));
            }
        }
    }
}