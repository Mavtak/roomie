using System;
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

        public int WriteData(params byte[] data)
        {
            if (data.Length > BackingObject.WriteLength)
            {
                throw new ArgumentException("data too long");
            }

            if (data.Length < BackingObject.WriteLength)
            {
                Array.Resize(ref data, BackingObject.WriteLength);
            }

            var result = BackingObject.WriteData(data);

            return result;
        }

        public int SetLeds(Led led, LightStatus status)
        {
            return WriteData(0, 179, (byte) led, (byte) status);
        }

        public int SetButtonLightsByRow(Bank bank, bool row1, bool row2, bool row3, bool row4, bool row5, bool row6)
        {
            byte value = 0;

            if (row1)
            {
                value += 1;
            }

            if (row2)
            {
                value += 2;
            }

            if (row3)
            {
                value += 4;
            }

            if (row4)
            {
                value += 8;
            }

            if (row5)
            {
                value += 16;
            }

            if (row6)
            {
                value += 32;
            }

            return WriteData(0, 182, (byte) bank, value);
        }

        public int SetAllButtonLights(Bank bank, bool on)
        {
            return SetButtonLightsByRow(bank, on, on, on, on, on, on);
        }

        public int SetButtonLight(Bank bank, byte rowIndex, byte columnIndex, LightStatus status)
        {
            var index = (byte) (rowIndex + columnIndex*8);

            if (bank == Bank.Red)
            {
                index += 32;
            }

            return WriteData(0, 181, index, (byte)status);
        }

        public int SetButtonLightIntensity(byte blue, byte red)
        {
            return WriteData(0, 187, blue, red);
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
