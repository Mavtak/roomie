using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using OpenZWaveDotNet;
using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Common.HomeAutomation.Exceptions;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWaveNetwork : Network
    {
        private readonly ZWManager _manager;
        private readonly List<OpenZWaveDevice> _devices;
        private readonly string _serialPortName;
        private UInt32? _homeId;
        private bool? _ready;

        public OpenZWaveNetwork(HomeAutomationNetworkContext context, string serialPortName)
            : base(context)
        {
            _serialPortName = serialPortName;

            _devices = new List<OpenZWaveDevice>();
            Devices = _devices;

            ConfigureOptions();

            _manager = new ZWManager();
            _manager.Create();
            _manager.OnNotification += OnNotification;

            Connect();
        }

        private void OnNotification(ZWNotification notification)
        {
            var homeId = notification.GetHomeId();
            var nodeId = notification.GetNodeId();
            var type = notification.GetType();

            if (_homeId == null)
            {
                _homeId = homeId;
                Address = "ZWave" + _homeId;
                Name = Address;
            }
            else if (_homeId != homeId)
            {
                throw new Exception("Unexpected Home ID");
            }

            if (type == ZWNotification.Type.NodeAdded)
            {
                var newDevice = new OpenZWaveDevice(this, _manager, nodeId);

                _devices.Add(newDevice);
                return;
            }

            var device = _devices.FirstOrDefault(x => x.Id == nodeId);
            var value = notification.GetValueID();

            switch (type)
            {
                case ZWNotification.Type.ValueAdded:
                    device.Values.Add(value);
                    break;

                case ZWNotification.Type.ValueChanged:
                    device.ProcessValueChanged(value);
                    break;

                case ZWNotification.Type.Notification:
                    _ready = true;
                    break;
            }
        }

        public override void Connect()
        {
            if (_ready != null)
            {
                return;
            }

            _ready = false;

            _manager.AddDriver(_serialPortName);

            //TODO: improve
            while (_ready != true)
            {
                Thread.Sleep(250);
            }

            Load();
            Connected();
        }

        public override void ScanForDevices()
        {
        }

        public override Device RemoveDevice()
        {
            throw new NotImplementedException();
        }

        public override Device AddDevice()
        {
            throw new NotImplementedException();
        }

        private static bool optionsConfigured = false;
        private const string ConfigurationPath = "OpenZWaveConfiguration";
        private static void ConfigureOptions()
        {
            if (optionsConfigured)
            {
                return;
            }

            var path = Path.GetFullPath(ConfigurationPath);

            if (!Directory.Exists(path))
            {
                throw new HomeAutomationException("Open Z-Wave configuration path not found at " + path);
            }

            optionsConfigured = true;

            var options = new ZWOptions();
            options.Create(path, string.Empty, string.Empty);

            // Add any app specific options here...
            options.AddOptionInt("SaveLogLevel", (int)ZWLogLevel.Detail);			// ordinarily, just write "Detail" level messages to the log
            options.AddOptionInt("QueueLogLevel", (int)ZWLogLevel.Debug);			// save recent messages with "Debug" level messages to be dumped if an error occurs
            options.AddOptionInt("DumpTriggerLevel", (int)ZWLogLevel.Error);		// only "dump" Debug  to the log emessages when an error-level message is logged

            // Lock the options
            options.Lock();
        }
    }
}
