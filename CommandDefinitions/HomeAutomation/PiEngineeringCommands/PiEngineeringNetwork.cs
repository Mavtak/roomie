﻿using System;
using System.Collections.Generic;
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

        public override void Connect()
        {
            ScanForDevices();
        }

        public override void ScanForDevices()
        {
            var count = 0;

            foreach (var piDevice in PIEDevice.EnumeratePIE())
            {
                if (piDevice.HidUsagePage == 0xc)
                {
                    count++;

                    var device = new PiEngineeringDevice(this, piDevice)
                    {
                        Address = count.ToString()
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
