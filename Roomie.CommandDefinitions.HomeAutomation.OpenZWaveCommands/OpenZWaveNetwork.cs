using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public void Connect()
        {
            using (var watcher = new ControllerNotificationWatcher(this))
            {
                Manager.AddDriver(_serialPortName);

                watcher.WaitUntilEventType(ZWNotification.Type.NodeQueriesComplete, ZWNotification.Type.EssentialNodeQueriesComplete);
            }

            Load();
            Connected();
        }

        public override Device RemoveDevice()
        {
            OpenZWaveDevice device = null;

            using (var watcher = new ControllerNotificationWatcher(this))
            {
                Manager.RemoveNode(HomeId.Value);

                watcher.ProcessChanges(notification =>
                {
                    if (notification.Type == ZWNotification.Type.NodeRemoved)
                    {
                        device = notification.Device;
                        return ControllerNotificationWatcher.ProcessAction.Quit;
                    }

                    return ControllerNotificationWatcher.ProcessAction.Continue;
                });
            }

            return device;
        }

        public override void RemoveDevice(Device device)
        {
            var zWaveDevice = device as OpenZWaveDevice;

            using (var watcher = new ControllerNotificationWatcher(this))
            {
                Manager.RemoveFailedNode(HomeId.Value, zWaveDevice.Id);

                watcher.WaitUntilEventType(ZWNotification.Type.NodeRemoved);
            }

            _devices.Remove(zWaveDevice);
        }

        public override Device AddDevice()
        {
            OpenZWaveDevice device = null;

            using (var watcher = new ControllerNotificationWatcher(this))
            {
                Manager.AddNode(HomeId.Value, true);

                watcher.ProcessChanges(notification =>
                {
                    if (notification.Type == ZWNotification.Type.NodeAdded)
                    {
                        device = notification.Device;
                        return ControllerNotificationWatcher.ProcessAction.Quit;
                    }

                    return ControllerNotificationWatcher.ProcessAction.Continue;
                });
            }

            return device;
        }

        public void OptimizePaths(bool returnRouteOptimization)
        {
            using (var stateWatcher = new ControllerNotificationWatcher(this))
            {
                Manager.HealNetwork(HomeId.Value, returnRouteOptimization);

                //TODO: figure out final state
                stateWatcher.LogChangesForever();
            }
        }

        public void Reset()
        {
            //TODO: clear Roomie representation of devices

            using (var stateWatcher = new ControllerNotificationWatcher(this))
            {
                Manager.ResetController(HomeId.Value);

                stateWatcher.WaitUntilEventType(ZWNotification.Type.NodeQueriesComplete, ZWNotification.Type.EssentialNodeQueriesComplete);
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
