﻿using System.Collections.Generic;
using PIEHidDotNet;
using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.Keypads.Buttons;

namespace Roomie.CommandDefinitions.PiEngineeringCommands
{
    public class PiEngineeringDevice : Device
    {
        public IEnumerable<IKeypadButtonState> Buttons { get; private set; }

        private readonly PiEngineeringKeypad _keypad;
        internal PIEDevice BackingObject { get; private set; }

        public PiEngineeringDevice(Network network, PIEDevice device, string name = null, ILocation location = null)
            : base(network, DeviceType.Keypad, name, location)
        {
            BackingObject = device;
            IsConnected = true;
            _keypad = new PiEngineeringKeypad(this);
        }

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
