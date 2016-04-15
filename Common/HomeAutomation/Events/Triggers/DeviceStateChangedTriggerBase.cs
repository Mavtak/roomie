using System.Collections.Generic;
using System.Linq;

namespace Roomie.Common.HomeAutomation.Events.Triggers
{
    public abstract class DeviceStateChangedTriggerBase<TState> : WhenDeviceEventHappensTrigger
    {
        protected DeviceStateChangedTriggerBase(IDevice device, IDeviceHistory history)
            : base(device, new DeviceStateChanged(), history)
        {
        }
        
        protected static IDeviceEvent GetEventBefore(IDeviceHistory history, IDeviceEvent target)
        {
            IDeviceEvent lastMatch = null;

            foreach (var @event in history)
            {
                if (@event == target)
                {
                    return lastMatch;
                }

                if (@event.Device == target.Device && @event.Type.Matches(target.Type))
                {
                    lastMatch = @event;
                }
            }

            return null;
        }

        protected override IEnumerable<IDeviceEvent> GetMatches(IDeviceHistory history, IDevice device, IEventType eventType, System.DateTime since)
        {
            var matches = base.GetMatches(history, device, eventType, since);

            matches = matches.Where(x => IsAMatch(history, x));

            return matches;
        }

        protected abstract TState GetMeasurement(IDeviceState state);


        private bool IsAMatch(IDeviceHistory history, IDeviceEvent candidate)
        {
            var previousEvent = GetEventBefore(history, candidate);
            var previousValue = GetMeasurement(previousEvent?.State);
            var currentValue = GetMeasurement(candidate?.State);

            if (previousValue == null || currentValue == null)
            {
                return false;
            }

            return IsAMatch(previousValue, currentValue);
        }
        
        protected abstract bool IsAMatch(TState previousValue, TState currentValue);
    }
}
