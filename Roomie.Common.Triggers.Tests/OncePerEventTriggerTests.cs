using System.Collections.Generic;
using NUnit.Framework;

namespace Roomie.Common.Triggers.Tests
{
    public class OncePerEventTriggerTests
    {
        [TestCaseSource("TestData")]
        public bool[] ItOnlyReturnsTrueForTheFirstInASequenceOfTrue(bool[] input)
        {
            var trigger = new OncePerEventTrigger(TriggerTestHelpers.SequenceTrigger(input));
            var actual = new bool[input.Length];

            for (var i = 0; i < input.Length; i++)
            {
                actual[i] = trigger.Check();
            }

            return actual;
        }

        public IEnumerable<TestCaseData> TestData
        {
            get
            {
                yield return new TestCaseData(new[] {true, false, false, false, false})
                    .SetName("true, false, false, false, false")
                    .Returns(new[] {true, false, false, false, false});

                yield return new TestCaseData(new[] {true, true, true, true, true})
                    .SetName("true, true, true, true, true, true")
                    .Returns(new[] {true, false, false, false, false});

                yield return new TestCaseData(new[] {true, true, false, true, false})
                    .SetName("true, true, false, true, false")
                    .Returns(new[] {true, false, false, true, false});

                yield return new TestCaseData(new[] {true, false, true, true, true, false, false, false, true, false})
                    .SetName("true, false, true, true, true, false, false, false, true, false")
                    .Returns(new[] {true, false, true, false, false, false, false, false, true, false});
            }
        }
    }
}
