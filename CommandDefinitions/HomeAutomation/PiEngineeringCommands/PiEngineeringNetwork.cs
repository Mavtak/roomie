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

            foreach (var piDevice in PIEDevice.EnumeratePIE())
            {
                if (piDevice.HidUsagePage == 0xc)
                {
                    var device = new PiEngineeringDevice(this, piDevice)
                    {
                        Address = (_devices.Count + 1).ToString()
                    };

                    _devices.Add(device);
                }
            }
        }

        public override Device RemoveDevice()
        {
            throw new NotImplementedException();
        }

        public override Device AddDevice()
        {
            throw new NotImplementedException();
        }
    }
}
