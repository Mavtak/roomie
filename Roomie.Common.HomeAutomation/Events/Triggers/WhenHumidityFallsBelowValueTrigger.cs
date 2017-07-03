using Roomie.Common.Measurements.Humidity;

namespace Roomie.Common.HomeAutomation.Events.Triggers
{
    public class WhenHumidityFallsBelowValueTrigger : DeviceStateChangedTriggerBase<IHumidity>
    {
        private readonly IHumidity _target;
        public WhenHumidityFallsBelowValueTrigger(IDevice device, IHumidity target, IDeviceHistory history)
            : base(device, history)
        {
            _target = target;
        }

        protected override IHumidity GetMeasurement(IDeviceState state)
        {
            return state?.HumiditySensorState?.Value;
        }

        protected override bool IsAMatch(IHumidity previousValue, IHumidity currentValue)
        {
            var wasAboveTarget = previousValue.Relative.Value >= _target.Relative.Value;
            var isBelowTarget = currentValue.Relative.Value < _target.Relative.Value;

            return wasAboveTarget && isBelowTarget;
        }
    }
}
