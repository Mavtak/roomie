using NUnit.Framework;

namespace Roomie.Common.Triggers.Tests
{
    [TestFixture]
    public class TriggerBundleTests
    {
        private ITrigger _trigger;
        private ITriggerAction[] _actions;
        private TriggerBundle _bundle;

        [SetUp]
        public void SetUp()
        {
            _trigger = TriggerTestHelpers.StaticTrigger(false);
            _actions = new[]
            {
                TriggerTestHelpers.DoNothingAction(),
                TriggerTestHelpers.DoNothingAction(),
                TriggerTestHelpers.DoNothingAction()
            };
            _bundle = new TriggerBundle(_trigger, _actions);
        }

        [Test]
        public void TheBundleSavesTheTrigger()
        {
            Assert.That(_bundle.Trigger, Is.SameAs(_trigger));
        }

        [Test]
        public void TheBundleSavesTheActions()
        {
            CollectionAssert.AreEqual(_actions, _bundle.Actions);
        }
    }
}
