using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Roomie.Common.Triggers.Tests.Fakes;

namespace Roomie.Common.Triggers.Tests
{
    public class OncePerTimespanTriggerTests
    {
        [TestCaseSource("TestData")]
        public bool [] ItWorks(TimeSpan timespan, Tuple<int, bool>[] events)
        {
            var innerTrigger = TriggerTestHelpers.SequenceTrigger(events.Select(x => x.Item2).ToArray());
            var now = DateTime.UtcNow;

            var trigger = new OncePerTimespanTriggerFake(innerTrigger, timespan);

            var results = new List<bool>();

            foreach(var @event in events)
            {
                trigger.SetNow(now.AddSeconds(@event.Item1));
                var result = trigger.Check();

                results.Add(result);
            }

            return results.ToArray();
        }

        public IEnumerable<TestCaseData> TestData
        {
            get
            {
                yield return new TestCaseData(new TimeSpan(0, 0, 0, 5), new Tuple<int, bool>[]
                    {
                        Tuple.Create(0, true),
                        Tuple.Create(1, false),
                    }).Returns(new[] {true, false});

                yield return new TestCaseData(new TimeSpan(0, 0, 0, 5), new Tuple<int, bool>[]
                    {
                        Tuple.Create(0, true),
                        Tuple.Create(1, true),
                    }).Returns(new[] {true, false});

                yield return new TestCaseData(new TimeSpan(0, 0, 0, 5), new Tuple<int, bool>[]
                    {
                        Tuple.Create(0, true),
                        Tuple.Create(1, true),
                        Tuple.Create(2, true),
                        Tuple.Create(3, true),
                        Tuple.Create(4, true),
                        Tuple.Create(5, true),
                        Tuple.Create(6, true),
                    }).Returns(new[] { true, false, false, false, false, false, true });

                yield return new TestCaseData(new TimeSpan(0, 0, 0, 5), new Tuple<int, bool>[]
                    {
                        Tuple.Create(0, false),
                        Tuple.Create(1, true),
                        Tuple.Create(2, true),
                        Tuple.Create(3, true),
                        Tuple.Create(4, true),
                        Tuple.Create(5, true),
                        Tuple.Create(6, true),
                        Tuple.Create(7, true),
                    }).Returns(new[] { false, true, false, false, false, false, false, true });
            }
        }
    }
}
