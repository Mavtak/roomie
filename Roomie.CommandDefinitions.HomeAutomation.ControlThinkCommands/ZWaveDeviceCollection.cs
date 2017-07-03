using System.Collections;
using System.Collections.Generic;
using Roomie.CommandDefinitions.HomeAutomationCommands;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    internal class ZWaveDeviceCollection : IEnumerable<ZWaveDevice>
    {
        private Dictionary<ControlThink.ZWave.Devices.ZWaveDevice, ZWaveDevice> backingDeviceIndex;

        public ZWaveDeviceCollection(ZWaveNetwork network)
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

        public void Add(Device device)
        {
            var zWaveDevice = (ZWaveDevice)device;
            backingDeviceIndex.Add(zWaveDevice.BackingObject, zWaveDevice);
        }

        public void Remove(Device device)
        {
            var zWaveDevice = (ZWaveDevice)device;
            backingDeviceIndex.Remove(zWaveDevice.BackingObject);
        }

        public IEnumerator<ZWaveDevice> GetEnumerator()
        {
            return backingDeviceIndex.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}   
