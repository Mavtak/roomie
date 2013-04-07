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

        public IEnumerator<IEvent> GetEnumerator()
        {
            //TODO: make this more efficient
            var result = ((IEnumerable<IEvent>) DeviceEvents).Concat(NetworkEvents).OrderBy(e => e.TimeStamp);

            return result.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
