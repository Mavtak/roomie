
using System.Linq;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.Keypads.Buttons;

namespace Roomie.Common.HomeAutomation.Events.Triggers
{
    public class WhenAKeypadButtonIsPressedTrigger : WhenDeviceEventHappensTrigger
    {
        private string _buttonId;

        public WhenAKeypadButtonIsPressedTrigger(IDevice device, string buttonId, IDeviceHistory history)
            : base(device, new DeviceStateChanged(), history)
        {
            _buttonId = buttonId;
        }

        protected override System.Collections.Generic.IEnumerable<IDeviceEvent> GetMatches(IDeviceHistory history, IDevice device, IEventType eventType, System.DateTime since)
        {
            var matches = base.GetMatches(history, device, eventType, since);

            matches = matches.Where(x => IsAMatch(history, x));

            return matches;
        }

        private bool IsAMatch(IDeviceHistory history, IDeviceEvent candidate)
        {
            var previous = GetEventBefore(history, candidate);
            IKeypadButtonState previousState = null;
            if (previous != null)
            {
                previousState = previous.State.KeypadState.Buttons.Get(_buttonId);
            }
            else
            {
                previousState = new ReadOnlyKeypadButtonState(_buttonId, null);
            }

            var currentState = candidate.State.KeypadState.Buttons.Get(_buttonId);

            if (currentState.Pressed != true)
            {
                return false;
            }

            var result = currentState.Changed(previousState);

            return result;
        }

        private static IDeviceEvent GetEventBefore(IDeviceHistory history, IDeviceEvent target)
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
    }
}
