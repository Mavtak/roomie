using System;
using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.CommandDefinitions.HomeAutomationCommands.Exceptions;
using Roomie.Common.HomeAutomation.Exceptions;
using Roomie.Desktop.Engine.WorkQueues;
using Device = Roomie.CommandDefinitions.HomeAutomationCommands.Device;
using Network = Roomie.CommandDefinitions.HomeAutomationCommands.Network;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    internal class ZWaveNetwork : Network
    {
        public global::ControlThink.ZWave.ZWaveController ZWaveController { get; private set; }
        public new ZWaveDeviceCollection Devices { get; private set; }
        public ParallelWorkQueue WorkQueue { get; private set; }

        public ZWaveNetwork(HomeAutomationNetworkContext context)
            : base(context)
        {
            ZWaveController = new global::ControlThink.ZWave.ZWaveController();
            this.Devices = new ZWaveDeviceCollection(this);
            base.Devices = this.Devices;

            WorkQueue = new ParallelWorkQueue();

            Connect();
        }

        public override void Connect()
        {
            try
            {
                if (ZWaveController.IsConnected)
                    return;

                ZWaveController.Connect();
                //TODO: clean this up
                Name = "ZWave" + ZWaveController.HomeID.ToString();
                Address = Name;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("not licensed"))
                    throw new SdkNotLicencedException();
                throw new HomeAutomationException("Error while connecting to the Z-Wave controller: " + e.Message);
            }

            ScanForDevices();

            //TODO: should this be elsewhere?
            Load();

            Connected();
        }

        public override void ScanForDevices()
        {
            foreach (global::ControlThink.ZWave.Devices.ZWaveDevice backingDevice in ZWaveController.Devices)
            {
                if (!Devices.Contains(backingDevice))
                {
                    Devices.Add(new ZWaveDevice(this, backingDevice));
                }
            }
        }

        public override Device RemoveDevice()
        {
            var removedDevice = ZWaveController.RemoveDevice();
            Device result;
            if (Devices.Contains(removedDevice))
            {
                result = Devices[removedDevice];
                Devices.Remove(result);
            }
            else
            {
                result = new ZWaveDevice(null, removedDevice);
            }

            return result;
        }

        public override Device AddDevice()
        {
            var backingDevice = ZWaveController.AddDevice();
            if (backingDevice == null)
                return null; //TODO: throw exception?

            var device = new ZWaveDevice(this, backingDevice);

            Devices.Add(device);
            return device;
        }
    }
}
