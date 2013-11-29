using System.Collections.Generic;
using System.Linq;
using Moq;

namespace Roomie.Common.Triggers.Tests
{
    public static class TriggerTestHelpers
    {
        public static ITrigger StaticTrigger(bool value)
        {
            var innerTrigger = new Mock<ITrigger>();
            innerTrigger.Setup(x => x.Check()).Returns(value);
            var trigger = innerTrigger.Object;

            return trigger;
        }

        public static IEnumerable<ITrigger> StaticTriggers(params bool[] values)
        {
            var result = values.Select(StaticTrigger);

            return result;
        }

        public static ITrigger SequenceTrigger(params bool[] values)
        {
            var result = new Mock<ITrigger>();
            var index = 0;

            result.Setup(x => x.Check()).Returns(() => values[index++]);

            return result.Object;
        }

        public static ITriggerAction DoNothingAction()
        {
            var result = new Mock<ITriggerAction>();
            result.Setup(x => x.Action());

            return result.Object;
        }
    }
}
