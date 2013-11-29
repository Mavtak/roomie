using System.Collections.Generic;
using NUnit.Framework;

namespace Roomie.Common.Triggers.Tests
{
    [TestFixture]
    public class OrTriggerTests
    {
        [TestCaseSource("TestData")]
        public bool ItReturnsTrueIfOneOfTheInnerTriggersAreTrue(IEnumerable<ITrigger> innerTriggers)
        {
            var trigger = new OrTrigger(innerTriggers);
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
                yield return CreateTestCaseData(true, true, false);
                yield return CreateTestCaseData(true, true, true, false);
                yield return CreateTestCaseData(true, true, false, true);
                yield return CreateTestCaseData(true, false, false, true);
            }
        }

        private static TestCaseData CreateTestCaseData(bool expected, params bool[] innerValues)
        {
            return new TestCaseData(TriggerTestHelpers.StaticTriggers(innerValues)).Returns(expected);
        }
    }
}
