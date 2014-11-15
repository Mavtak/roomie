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
            ScanForDevices();
            Connected();
        }

        public void ScanForDevices()
        {
            if (_devices.Any())
            {
                return;
            }

            var backingObjects = PIEDevice.EnumeratePIE()
                .Where(x => x.HidUsagePage == 0xc);

            foreach (var backingObject in backingObjects)
            {
                var device = new PiEngineeringDevice(this, backingObject)
                    {
                        Address = (_devices.Count + 1).ToString()
                    };

                _devices.Add(device);
            }
        }

        public override Device RemoveDevice()
        {
            throw new NotImplementedException();
        }

        public override void RemoveDevice(Device device)
        {
            throw new NotImplementedException();
        }

        public override Device AddDevice()
        {
            throw new NotImplementedException();
        }
    }
}
