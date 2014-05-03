using System;
using Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.General;
using Roomie.Common.HomeAutomation.Events;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific
{
    public class SwitchBinaryDataEntry : BoolNodeDataEntry
    {
        public SwitchBinaryDataEntry(OpenZWaveDevice device)
            : base(device, CommandClass.SwitchBinary)
        {
        }

        protected override IDeviceEvent CreateDeviceEvent()
        {
            var state = GetValue();

            if (state == true)
            {
                return DeviceEvent.PoweredOff(Device, null);
            }

            if (state == false)
            {
                return DeviceEvent.PoweredOff(Device, null);
            }

            throw new Exception("unexpected state");
        }
    }
}
