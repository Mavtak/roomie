using NUnit.Framework;

namespace Roomie.Common.Triggers.Tests
{
    [TestFixture]
    public class TriggerCollectionTests
    {
        [Test]
        public void ItOnlyReturnsTriggeredBundles()
        {
            var collection = new TriggerCollection();

            var bundles = new[]
            {
                new TriggerBundle(TriggerTestHelpers.StaticTrigger(true)),
                new TriggerBundle(TriggerTestHelpers.StaticTrigger(false)),
                new TriggerBundle(TriggerTestHelpers.StaticTrigger(true)),
                new TriggerBundle(TriggerTestHelpers.StaticTrigger(false)),
                new TriggerBundle(TriggerTestHelpers.StaticTrigger(false))
            };

            var expected = new[]
            {
                bundles[0],
                bundles[2],
            };

            foreach(var bundle in bundles)
            {
                collection.Add(bundle);
            }

            var actual = collection.Check();

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
