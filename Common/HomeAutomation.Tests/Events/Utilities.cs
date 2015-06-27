using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Roomie.Common.HomeAutomation.Events;

namespace Roomie.Common.HomeAutomation.Tests.Events
{
    public static class Utilities
    {
        [ThreadStatic]
        public static readonly Random Random = new Random();

        public static IEnumerable<DateTime> GenerateTimeStamps()
        {
            var lastTime = new DateTime(1, 1, 1);

            var count = Random.Next(50, 200);

            for (var i = 0; i < count; i++)
            {
                var thisTime = lastTime.AddSeconds(Random.Next(0, 3));
                yield return thisTime;

                lastTime = thisTime;
            }
        }

        public static IEnumerable<TEvent> GenerateEvents<TEvent>(Action<Mock<TEvent>> customizations = null)
            where TEvent : class, IEvent
        {
            foreach (var timestamp in Utilities.GenerateTimeStamps())
            {
                var eventMock = new Mock<TEvent>();
                eventMock.Setup(e => e.TimeStamp).Returns(timestamp);

                if (customizations != null)
                {
                    customizations(eventMock);
                }

                yield return eventMock.Object;
            }
        }

        public static T RandomElement<T>(this Random random, IList<T> collection)
        {
            var result = collection[random.Next(0, collection.Count)];

            return result;
        }

        public static T RandomElement<T>(this Random random, IEnumerable<T> collection)
        {
            return random.RandomElement(collection.ToList());
        }
    }
}
