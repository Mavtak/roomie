using System.Collections.Generic;
using Roomie.CommandDefinitions.ControlThinkCommands;
using Roomie.CommandDefinitions.HomeAutomationCommands;


namespace Roomie.CommandDefinitions.ZWave.ControlThinkCommands
{
    public class ZWaveDeviceCollection : DeviceCollection
    {
        private Dictionary<ControlThink.ZWave.Devices.ZWaveDevice, ZWaveDevice> backingDeviceIndex;

        public ZWaveDeviceCollection(ZWaveNetwork network)
            : base(network)
        {
            backingDeviceIndex = new Dictionary<global::ControlThink.ZWave.Devices.ZWaveDevice, ZWaveDevice>();
        }
        public bool Contains(global::ControlThink.ZWave.Devices.ZWaveDevice backingDeviceObject)
        {
            return (backingDeviceObject != null) && backingDeviceIndex.ContainsKey(backingDeviceObject);
        }

        public Device this[global::ControlThink.ZWave.Devices.ZWaveDevice backingDevice]
        {
            get
            {
                return backingDeviceIndex[backingDevice];
            }
        }

        protected override void DeviceAdded(Device device)
        {
            var zWaveDevice = (ZWaveDevice)device;
            backingDeviceIndex.Add(zWaveDevice.BackingObject, zWaveDevice);
        }

        protected override void DeviceRemoved(Device device)
        {
            var zWaveDevice = (ZWaveDevice)device;
            backingDeviceIndex.Remove(zWaveDevice.BackingObject);
        }

    }
}   
