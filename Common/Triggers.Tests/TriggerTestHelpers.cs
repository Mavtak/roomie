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
    }
}
