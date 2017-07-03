using System;

namespace Roomie.Common.HomeAutomation.Events.Triggers
{
    public class WhenTheCurrentActionChangesTrigger : DeviceStateChangedTriggerBase<string>
    {
        private readonly string _target;

        public WhenTheCurrentActionChangesTrigger(IDevice device, string target, IDeviceHistory history)
            : base(device, history)
        {
            _target = target;
        }

        protected override string GetMeasurement(IDeviceState state)
        {
            return state?.CurrentAction;
        }

        protected override bool IsAMatch(string previousValue, string currentValue)
        {
            var changed = !string.Equals(currentValue, previousValue);
            var isTarget = string.Equals(currentValue, _target, StringComparison.InvariantCulture);

            return changed && isTarget;
        }
    }
}
