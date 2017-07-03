using System.Linq;
using NUnit.Framework;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.Measurements.Power;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Tests
{
    public class CurrentStateGeneratorTests
    {
        [TestCase("", BinarySwitchPower.On, 0, null)]
        [TestCase("", BinarySwitchPower.On, 5, null)]
        [TestCase("", BinarySwitchPower.Off, 0, null)]
        [TestCase("1,off;10,idle;100,running", BinarySwitchPower.Off, 0, null)]
        [TestCase("1,off;10,idle;100,running", BinarySwitchPower.On, 0, "off")]
        [TestCase("1,off;10,idle;100,running", BinarySwitchPower.On, 1, "off")]
        [TestCase("1,off;10,idle;100,running", BinarySwitchPower.On, 5, "idle")]
        [TestCase("1,off;10,idle;100,running", BinarySwitchPower.On, 10, "idle")]
        [TestCase("1,off;10,idle;100,running", BinarySwitchPower.On, 50, "running")]
        [TestCase("1,off;10,idle;100,running", BinarySwitchPower.On, 100, "running")]
        [TestCase("1,off;10,idle;100,running", BinarySwitchPower.On, 101, null)]
        [TestCase("100,running;1,off", BinarySwitchPower.On, 0, "off")]
        public void Works(string steps, BinarySwitchPower? binaryPower, double wattsPower, string expected)
        {
            var generator = new CurrentStateGenerator();

            if (!string.IsNullOrEmpty(steps))
            {
                foreach (var step in steps.Split(';').Select(x => x.Split(',')))
                {
                    var stepValue = double.Parse(step[0]);
                    var stepName = step[1];
                    generator.AddStep(new WattsPower(stepValue), stepName);
                }
            }

            var actual = generator.Generate(binaryPower, new WattsPower(wattsPower));

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
