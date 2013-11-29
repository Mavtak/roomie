using NUnit.Framework;

namespace Roomie.Common.Triggers.Tests
{
    [TestFixture]
    public class NotTriggerTests
    {
        [TestCase(true, false)]
        [TestCase(false, true)]
        public void ItReturnsTheOppositeOfTheInternalTrigger(bool inner, bool expected)
        {
            var innerTrigger = TriggerTestHelpers.StaticTrigger(inner);
            var trigger = new NotTrigger(innerTrigger);
            var actual = trigger.Check();

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
