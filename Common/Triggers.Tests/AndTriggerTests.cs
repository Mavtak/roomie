using System.Collections.Generic;
using NUnit.Framework;

namespace Roomie.Common.Triggers.Tests
{
    [TestFixture]
    public class AndTriggerTests
    {
        [TestCaseSource("TestData")]
        public bool ItReturnsTrueOnlyIfAllOfTheInnerTriggersAreTrue(IEnumerable<ITrigger> innerTriggers)
        {
            var trigger = new AndTrigger(innerTriggers);
            var actual = trigger.Check();

            return actual;
        }

        public IEnumerable<TestCaseData> TestData
        {
            get
            {
                yield return CreateTestCaseData(true, true);
                yield return CreateTestCaseData(true, true, true);
                yield return CreateTestCaseData(true, true, true, true);
                yield return CreateTestCaseData(false, false);
                yield return CreateTestCaseData(false, false, false);
                yield return CreateTestCaseData(false, false, false, false);
                yield return CreateTestCaseData(false, true, false);
                yield return CreateTestCaseData(false, true, true, false);
                yield return CreateTestCaseData(false, true, false, true);
            }
        }

        private static TestCaseData CreateTestCaseData(bool expected, params bool[] innerValues)
        {
            return new TestCaseData(TriggerTestHelpers.StaticTriggers(innerValues)).Returns(expected);
        }
    }
}
