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

        protected override IDeviceEvent CreateDeviceEvent()
        {
            return DeviceEvent.ThermostatSetpointsChanged(Device, null);
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
