using System;
using Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.General;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific
{
    public class ThermostatSetpointDataEntry : WritableTemperatureNodeDataEntry
    {
        public const byte HeatSetpointIndex = 1;
        public const byte CoolSetpointIndex = 2;

        public ThermostatSetpointDataEntry(OpenZWaveDevice device, ThermostatSetpointType setpointType)
            : base(device, CommandClass.ThermostatSetpoint, GetSetpointIndex(setpointType))
        {
        }

        public override bool ProcessValueChanged(OpenZWaveDeviceValue entry)
        {
            if (!Matches(entry))
            {
                return false;
            }

            var @event = DeviceEvent.ThermostatSetpointsChanged(Device, null);
            Device.AddEvent(@event);

            return true;
        }

        private static byte GetSetpointIndex(ThermostatSetpointType setpointType)
        {
            switch (setpointType)
            {
                case ThermostatSetpointType.Heat:
                    return HeatSetpointIndex;

                case ThermostatSetpointType.Cool:
                    return CoolSetpointIndex;
            }

            throw new ArgumentException("setpointType " + setpointType + " not recognized", "setpointType");
        }
    }
}
