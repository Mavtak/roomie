using System;
using System.Collections.Generic;
using System.Linq;
using PIEHidDotNet;
using Roomie.CommandDefinitions.HomeAutomationCommands;

namespace Roomie.CommandDefinitions.PiEngineeringCommands
{
    public class PiEngineeringNetwork : Network
    {
        private List<PiEngineeringDevice> _devices; 

        public PiEngineeringNetwork(HomeAutomationNetworkContext context)
            : base(context)
        {
            _devices = new List<PiEngineeringDevice>();
            base.Devices = _devices;

            Address = "PiEngineeringDevices";
            Name = Address;

            Connect();
        }

        public void Connect()
        {
            ScanForNewDevices();
            Load();
            Connected();
        }

        private IEnumerable<PIEDevice> ScanForBackingDevices()
        {
            var result = PIEDevice.EnumeratePIE()
                .Where(x => x.HidUsagePage == 0xc);

            return result;
        }

        private IEnumerable<PIEDevice> ScanForNewBackingDevices()
        {
            var backingDevices = ScanForBackingDevices();
            var result = backingDevices
                .Where(x => !_devices.Any(y => string.Equals(y.BackingObject.Path, x.Path)));

            return result;
        }

        public IEnumerable<PiEngineeringDevice> ScanForNewDevices()
        {
            var backingObjects = ScanForNewBackingDevices();
            var newDevices = new List<PiEngineeringDevice>();

            foreach (var backingObject in backingObjects)
            {
                var device = new PiEngineeringDevice(this, backingObject)
                    {
                        Address = (_devices.Count + 1).ToString()
                    };

                _devices.Add(device);
                newDevices.Add(device);
            }

            return newDevices;
        }

        public override Device RemoveDevice()
        {
            throw new NotImplementedException();
        }

        public override void RemoveDevice(Device device)
        {
            throw new NotImplementedException();
        }

        //TODO update this "AddDevice" interface to understand that multiple devices might be added.
        public override Device AddDevice()
        {
            var result = ScanForNewDevices().FirstOrDefault();

            return result;
        }
    }
}
