using Roomie.Common.HomeAutomation.Keypads.Buttons;

namespace Roomie.Common.HomeAutomation.Events.Triggers
{
    public class WhenAKeypadButtonIsPressedTrigger : DeviceStateChangedTriggerBase<IKeypadButtonState>
    {
        private readonly string _buttonId;

        public WhenAKeypadButtonIsPressedTrigger(IDevice device, string buttonId, IDeviceHistory history)
            : base(device, history)
        {
            _buttonId = buttonId;
        }

        protected override IKeypadButtonState GetMeasurement(IDeviceState state)
        {
            return state?.KeypadState?.Buttons?.Get(_buttonId);
        }

        protected override bool IsAMatch(IKeypadButtonState previousValue, IKeypadButtonState currentValue)
        {
            return currentValue.Changed(previousValue);
        }
    }
}
