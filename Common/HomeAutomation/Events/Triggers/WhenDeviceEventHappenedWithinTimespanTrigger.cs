using System;

namespace Roomie.Common.HomeAutomation.Events.Triggers
{
    public class WhenDeviceEventHappenedWithinTimespanTrigger : WhenDeviceEventHappensTrigger
    {
        private TimeSpan _timeSpan;

        public WhenDeviceEventHappenedWithinTimespanTrigger(IDevice device, IEventType deviceType, TimeSpan timeSpan, IDeviceHistory history)
            : base(device, deviceType, history)
        {
            _timeSpan = timeSpan;
        }

        protected override DateTime UpdateLastCheck()
        {
            var now = GetNow();
            var result = now.Subtract(_timeSpan);

            return result;
        }
    }
}
