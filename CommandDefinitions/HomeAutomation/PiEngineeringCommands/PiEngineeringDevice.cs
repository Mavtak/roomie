using System.Collections.Generic;
using PIEHidDotNet;
using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.Keypads.Buttons;

namespace Roomie.CommandDefinitions.PiEngineeringCommands
{
    public class PiEngineeringDevice : Device, PIEErrorHandler
    {
        public IEnumerable<IKeypadButtonState> Buttons { get; private set; }

        private readonly PiEngineeringKeypad _keypad;
        internal PIEDevice BackingObject { get; private set; }

        public PiEngineeringDevice(Network network, PIEDevice device, string name = null, ILocation location = null)
            : base(network, DeviceType.Keypad, name, location)
        {
            BackingObject = device;
            _keypad = new PiEngineeringKeypad(this);

            BackingObject.SetErrorCallback(this);

            Reconnect();
        }

        public void Reconnect()
        {
            BackingObject.SetupInterface(false);

            HandleDeviceConnected(BackingObject.Connected);
        }

        private void HandleDeviceConnected(bool connected)
        {
            if (IsConnected == connected)
            {
                return;
            }

            IsConnected = connected;

            var @event = connected ? DeviceEvent.Found(this, null) : DeviceEvent.Lost(this, null);
            AddEvent(@event);
        }

        #region PIEErrorHandler implementation

        public void HandlePIEHidError(int error, PIEDevice sourceDevice)
        {
            if (sourceDevice != BackingObject)
            {
                return;
            }

            HandleDeviceConnected(error != 301 && BackingObject.Connected);
        }

        #endregion

        #region Device overrides

        public override IKeypad Keypad
        {
            get
            {
                return _keypad;
            }
        }

        #endregion
    }
}
