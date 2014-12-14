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
        public ZWManager Manager { get; private set; }
        private readonly List<OpenZWaveDevice> _devices;
        private readonly string _serialPortName;
        public UInt32? HomeId { get; private set; }
        private bool? _ready;
        private OpenZWaveNotificationProcessor _notificationProcessor;

        public OpenZWaveNetwork(HomeAutomationNetworkContext context, string serialPortName)
            : base(context)
        {
            _serialPortName = serialPortName;

            _devices = new List<OpenZWaveDevice>();
            Devices = _devices;
            _notificationProcessor = new OpenZWaveNotificationProcessor(this);

            ConfigureOptions();

            Manager = new ZWManager();
            Manager.Create();
            Manager.OnNotification += OnNotification;

            Connect();
        }

        private void OnNotification(ZWNotification notification)
        {
            var notification2 = new OpenZWaveNotification(this, notification);

            _notificationProcessor.Process(notification2);
        }

        internal void SetHomeId(UInt32 homeId)
        {
            if (HomeId != null)
            {
                throw new Exception("Home ID already set.");
            }

            HomeId = homeId;
            Address = "ZWave" + HomeId;
            Name = Address;
        }

        internal void AddDevice(OpenZWaveDevice device)
        {
            _devices.Add(device);
        }

        internal OpenZWaveDevice GetDevice(byte id)
        {
            var result = _devices.FirstOrDefault(x => x.Id == id);

            return result;
        }

        internal void SetReady()
        {
            _ready = true;
        }

        public void Connect()
        {
            if (_ready != null)
            {
                return;
            }

            _ready = false;

            Manager.AddDriver(_serialPortName);

            //TODO: improve
            while (_ready != true)
            {
                Thread.Sleep(250);
            }

            Load();
            Connected();
        }

        public override Device RemoveDevice()
        {
            using (var watcher = new ControllerStateWatcher(this))
            {
                Manager.BeginControllerCommand(HomeId.Value, ZWControllerCommand.RequestNetworkUpdate, true, 0);
                watcher.ProcessChanges();
            }

            return null;
        }

        public override void RemoveDevice(Device device)
        {
            var zWaveDevice = device as OpenZWaveDevice;

            using (var watcher = new ControllerStateWatcher(this))
            {
                Manager.BeginControllerCommand(HomeId.Value, ZWControllerCommand.RemoveFailedNode, false, zWaveDevice.Id);
                watcher.ProcessChanges();
            }

            _devices.Remove(zWaveDevice);
        }

        public override Device AddDevice()
        {
            using (var watcher = new ControllerStateWatcher(this))
            {
                Manager.BeginControllerCommand(HomeId.Value, ZWControllerCommand.AddDevice, true, 0);
                watcher.ProcessChanges();
            }

            return null;
        }

        public void OptimizePaths(bool returnRouteOptimization)
        {
            using (var stateWatcher = new ControllerStateWatcher(this))
            {
                Manager.HealNetwork(HomeId.Value, returnRouteOptimization);
                stateWatcher.ProcessChanges();
            }
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
            options.AddOptionInt("SaveLogLevel", (int)ZWLogLevel.Error);			// ordinarily, just write "Detail" level messages to the log
            options.AddOptionInt("QueueLogLevel", (int)ZWLogLevel.Debug);			// save recent messages with "Debug" level messages to be dumped if an error occurs
            options.AddOptionInt("DumpTriggerLevel", (int)ZWLogLevel.Error);		// only "dump" Debug  to the log emessages when an error-level message is logged

            // Lock the options
            options.Lock();
        }
    }
}
