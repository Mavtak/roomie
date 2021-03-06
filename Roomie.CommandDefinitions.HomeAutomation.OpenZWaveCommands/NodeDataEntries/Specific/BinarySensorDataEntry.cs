﻿using Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.General;
using Roomie.Common.HomeAutomation.Events;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific
{
    public class BinarySensorDataEntry : BoolNodeDataEntry
    {
        public BinarySensorDataEntry(OpenZWaveDevice device)
            : base(device, CommandClass.SensorBinary, false)
        {
        }

        public override bool? GetValue()
        {
            if (!LastUpdated.HasValue)
            {
                return null;
            }

            return base.GetValue();
        }

        protected override IDeviceEvent CreateDeviceEvent()
        {
            return DeviceEvent.BinarySensorValueChanged(Device, null);
        }
    }
}
