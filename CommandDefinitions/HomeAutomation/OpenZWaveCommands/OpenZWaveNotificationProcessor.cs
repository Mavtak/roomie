using System;
using System.Diagnostics;
using System.Linq;
using OpenZWaveDotNet;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.Events;
using NotificationType = OpenZWaveDotNet.ZWNotification.Type;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWaveNotificationProcessor
    {
        private readonly OpenZWaveNetwork _network;
        private bool _networkReady;

        public OpenZWaveNotificationProcessor(OpenZWaveNetwork network)
        {
            _network = network;
            _networkReady = false;
        }

        public void Process(OpenZWaveNotification notification)
        {
            if (_network.HomeId == null)
            {
                _network.SetHomeId(notification.HomeId);

            }

            if (_network.HomeId != notification.HomeId)
            {
                throw new Exception("Unexpected Home ID");
            }

            if (!_networkReady && notification.Type != NotificationType.DriverReady && notification.Type != NotificationType.NodeNew && notification.Type != NotificationType.NodeAdded)
            {
                _network.SetReady();
            }

            Debug.WriteLine(notification.NodeId + "\t" + notification.Type + "\t" + notification.Event + "\t" + notification.Value.GetValue());

            //TODO: fill in other cases
            switch (notification.Type)
            {
                case NotificationType.AllNodesQueried:
                    _network.Log("All nodes queried");
                    break;

                case NotificationType.AllNodesQueriedSomeDead:
                    _network.Log("All nodes queried, some dead");
                    break;

                case NotificationType.AwakeNodesQueried:
                    _network.Log("All awake nodes queried");
                    break;

                case NotificationType.ButtonOff:
                case NotificationType.ButtonOn:
                case NotificationType.CreateButton:
                case NotificationType.DeleteButton:
                case NotificationType.DriverFailed:
                case NotificationType.DriverReady:
                case NotificationType.DriverReset:
                case NotificationType.EssentialNodeQueriesComplete:
                case NotificationType.Group:
                    break;

                case NotificationType.NodeAdded:
                    var newDevice = new OpenZWaveDevice(_network, _network.Manager, notification.NodeId);
                    _network.AddDevice(newDevice);
                    break;

                case NotificationType.NodeEvent:
                    var device = notification.Device;

                    HandleDeviceConnected(device, true);

                    device.Event.Update(notification.Event);
                    break;

                case NotificationType.NodeNaming:
                    Nodification("node named", notification);
                    break;

                case NotificationType.NodeNew:
                    Nodification("new device", notification);
                    break;

                case NotificationType.NodeProtocolInfo:
                    Nodification("protocol info", notification);
                    break;

                case NotificationType.NodeQueriesComplete:
                    _network.Log("node queries complete");
                    break;

                case NotificationType.NodeRemoved:
                    Nodification("removed", notification);
                    break;

                case NotificationType.Notification:
                    HandleDeviceConnected(notification.Device, false);
                    break;

                case NotificationType.PollingDisabled:
                    Nodification("polling disabled", notification);
                    break;

                case NotificationType.PollingEnabled:
                    Nodification("polling enabled", notification);
                    break;

                case NotificationType.SceneEvent:
                    break;

                case NotificationType.ValueAdded:
                    notification.Device.Values.Add(notification.Value);
                    HandleDeviceValueUpdated(notification.Device, notification.Value, ValueUpdateType.Add);
                    break;

                case NotificationType.ValueRefreshed:
                    HandleDeviceValueUpdated(notification.Device, notification.Value, ValueUpdateType.Refresh);
                    break;

                case NotificationType.ValueChanged:
                    HandleDeviceValueUpdated(notification.Device, notification.Value, ValueUpdateType.Change);
                    break;

                case NotificationType.ValueRemoved:
                    notification.Device.RemoveValue(notification.Value);
                    HandleDeviceValueUpdated(notification.Device, notification.Value, ValueUpdateType.Remove);
                    break;

                default:
                    throw new Exception("Unexpected notification type " + notification.Type);
            }
        }

        private void HandleDeviceConnected(OpenZWaveDevice device, bool connected)
        {
            if (device.IsConnected == connected)
            {
                return;
            }

            device.IsConnected = connected;

            var @event = connected ? DeviceEvent.Found(device, null) : DeviceEvent.Lost(device, null);
            device.AddEvent(@event);
        }

        private void HandleDeviceValueUpdated(OpenZWaveDevice device, OpenZWaveDeviceValue value, ValueUpdateType updateType)
        {
            HandleDeviceConnected(device, true);
            device.ProcessValueUpdate(value, updateType);
        }

        private void Nodification(string operation, OpenZWaveNotification notification)
        {
            string deviceString;
            var device = notification.Device;
            var id = notification.NodeId;

            if (device != null)
            {
                deviceString = device.BuildVirtualAddress(false, false);
            }
            else if (id > 0)
            {
                deviceString = "device id " + id;
            }
            else
            {
                deviceString = "unknown device";
            }

            var message = string.Format("Node {0}: {1}", operation, deviceString);

            _network.Log(message);
        }
    }
}
