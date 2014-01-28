using System;
using OpenZWaveDotNet;
using Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.General;
using Roomie.Common.HomeAutomation.Events;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific
{
    public class SwitchBinaryDataEntry : BoolNodeDataEntry
    {
        public SwitchBinaryDataEntry(OpenZWaveDevice device)
            : base(device, (byte) CommandClass.SwitchBinary)
        {
        }

        public override bool ProcessValueChanged(ZWValueID entry)
        {
            if (entry.GetCommandClassId() != (byte)CommandClass.SwitchBinary)
            {
                return false;
            }

            var state = GetValue();

            IDeviceEvent @event;

            if (state == true)
            {
                @event = DeviceEvent.PoweredOff(Device, null);
            }
            else if (state == false)
            {
                @event = DeviceEvent.PoweredOff(Device, null);
            }
            else
            {
                throw new Exception("unexpected state");
            }

            Device.AddEvent(@event);

            return true;
        }
    }
}
