using System;
using System.Linq;
using Roomie.Common.Triggers;

namespace Roomie.Common.HomeAutomation.Events.Triggers
{
    public class WhenDeviceEventHappensTrigger : ITrigger
    {
        private IDevice _device;
        private IEventType _eventType;
        private IDeviceHistory _history;
        private DateTime _lastCheck = DateTime.UtcNow;

        public WhenDeviceEventHappensTrigger(IDevice device, IEventType deviceType, IDeviceHistory history)
        {
            _device = device;
            _eventType = deviceType;
            _history = history;
            _lastCheck = UpdateLastCheck();
        }

        public bool Check()
        {
            var result = HistoryContainsEvent(_history, _device, _eventType, _lastCheck);
            _lastCheck = UpdateLastCheck();

            return result;
        }

        protected virtual DateTime UpdateLastCheck()
        {
            return GetNow();
        }

        protected virtual DateTime GetNow()
        {
            return DateTime.UtcNow;
        }

        public static bool HistoryContainsEvent(IDeviceHistory history, IDevice device, IEventType eventType, DateTime since)
        {
            var matches = history.Where(x => x.TimeStamp >= since);
            matches = matches.Where(x => x.Device.Equals(device));
            matches = matches.Where(x => x.Type.Matches(eventType));
            var result = matches.Any();

            return result;
        }
    }
}
