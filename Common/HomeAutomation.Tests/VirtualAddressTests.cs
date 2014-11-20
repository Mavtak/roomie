using NUnit.Framework;

namespace Roomie.Common.HomeAutomation.Tests
{
    public class VirtualAddressTests
    {
        //TODO: test "IsValid"

        [TestCase("derp", null, null, null, null, "derp", null)]
        [TestCase("Derp", null, null, null, null, "Derp", null)]
        [TestCase("123", null, null, null, null, "123", null)]
        [TestCase("derp (herp)", null, null, null, null, "derp (herp)", null)]
        [TestCase("herp & derp", null, null, null, null, "herp & derp", null)]
        [TestCase("herp: derp", null, null, null, "herp", "derp", null)]
        [TestCase("herp/derp", null, "herp", null, null, "derp", null)]
        [TestCase("Herp/Derp", null, "Herp", null, null, "Derp", null)]
        [TestCase("1herp2/3derp4", null, "1herp2", null, null, "3derp4", null)]
        [TestCase("123/456", null, "123", null, null, "456", null)]
        [TestCase("herp a/derp a", null, "herp a", null, null, "derp a", null)]
        [TestCase("[derp]", null, null, null, null, null, "derp")]
        [TestCase("[123]", null, null, null, null, null, "123")]
        [TestCase("[herp]/derp[boop]", null, null, "herp", null, "derp", "boop")]
        [TestCase("[herp]/derp [boop]", null, null, "herp", null, "derp", "boop")]
        [TestCase("herp: derp/beep: boop", "herp", "derp", null, "beep", "boop", null)]
        [TestCase("a: b[c]/d: e[f]", "a", "b", "c", "d", "e", "f")]
        [TestCase("a: b [c]/d: e [f]", "a", "b", "c", "d", "e", "f")]
        public void WorksForValidAddresses(string input, string networkLocation, string networkName, string networkId, string deviceLocation, string deviceName, string deviceId)
        {
            var result = VirtualAddress.Parse(input);

            Assert.That(result.NetworkLocation, Is.EqualTo(networkLocation));
            Assert.That(result.NetworkName, Is.EqualTo(networkName));
            Assert.That(result.NetworkNodeId, Is.EqualTo(networkId));

            Assert.That(result.DeviceLocation, Is.EqualTo(deviceLocation));
            Assert.That(result.DeviceName, Is.EqualTo(deviceName));
            Assert.That(result.DeviceNodeId, Is.EqualTo(deviceId));
        }

        [TestCase("_herp/derp")]
        [TestCase(" herp/derp")]
        [TestCase("he!rp/derp")]
        [TestCase("herp/de!rp")]
        [TestCase("he\\rp/derp")]
        [TestCase("herp/de\\rp")]
        [TestCase("a/b/c")]
        [TestCase("herp (derp(")]
        [TestCase(")herp")]
        [TestCase("herp &")]
        [TestCase("a&")]
        [TestCase("&")]
        [TestCase("a : b [c]")]
        public void FailsForInvalidAddresses(string input)
        {
            var result = VirtualAddress.Parse(input);

            Assert.That(result, Is.Null);
        }
    }
}
