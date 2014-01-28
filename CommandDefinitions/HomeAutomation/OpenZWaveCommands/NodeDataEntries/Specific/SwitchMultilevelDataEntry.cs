﻿using OpenZWaveDotNet;
using Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.General;
using Roomie.Common.HomeAutomation.Events;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific
{
    public class SwitchMultilevelDataEntry : ByteNodeDataEntry
    {
        public SwitchMultilevelDataEntry(OpenZWaveDevice device)
            : base(device, (byte)CommandClass.SwitchMultilevel)
        {
        }

        public override bool ProcessValueChanged(ZWValueID entry)
        {
            if (entry.GetCommandClassId() != (byte)CommandClass.SwitchMultilevel)
            {
                return false;
            }

            var @event = DeviceEvent.PowerChanged(Device, null);

            Device.AddEvent(@event);

            return true;
        }
    }
}