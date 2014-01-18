using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Roomie.Common.HomeAutomation.Events
{
    public class MasterHistory : IMasterHistory
    {
        public IDeviceHistory DeviceEvents { get; private set; }
        public INetworkHistory NetworkEvents { get; private set; }

        public MasterHistory(IDeviceHistory deviceHistory, INetworkHistory networkHistory)
        {
            DeviceEvents = deviceHistory;
            NetworkEvents = networkHistory;
        }

        public void Add(IEvent @event)
        {
            if (@event is IDeviceEvent)
            {
                var deviceEvent = (IDeviceEvent)@event;
                DeviceEvents.Add(deviceEvent);
                return;
            }

            if (@event is INetworkEvent)
            {
                var networkEvent = (INetworkEvent)@event;
                NetworkEvents.Add(networkEvent);
                return;
            }

            throw new Exception("Unknown event type " + @event.GetType());
        }

        public IEnumerable<IEvent> GetMatches(params Func<IEvent, bool>[] filters)
        {
            var deviceMatches = DeviceEvents.GetMatches(filters).Cast<IEvent>();
            var networkEvents = NetworkEvents.GetMatches(filters).Cast<IEvent>();

            var result = deviceMatches.Union(networkEvents);
            result = result.OrderBy(x => x.TimeStamp);
            result = result.ToArray();

            return result;
        }

        public IEnumerator<IEvent> GetEnumerator()
        {
            var sources = new []
            {
                DeviceEvents.Select(x => x as IEvent).GetEnumerator(),
                NetworkEvents.Select(x => x as IEvent).GetEnumerator()
            };

            var more = new bool[sources.Length];

            for (var i = 0; i < sources.Length; i++)
            {
                more[i] = sources[i].MoveNext();
            }

            while (more.Contains(true))
            {
                var smallestIndex = -1;

                for (var i = 0; i < sources.Length; i++)
                {
                    if (!more[i])
                    {
                        continue;
                    }

                    if (smallestIndex == -1 || sources[i].Current.TimeStamp < sources[smallestIndex].Current.TimeStamp)
                    {
                        smallestIndex = i;
                    }
                };

                yield return sources[smallestIndex].Current;

                more[smallestIndex] = sources[smallestIndex].MoveNext();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
