using System;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.Events;
using NotificationType = OpenZWaveDotNet.ZWNotification.Type;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWaveNotificationProcessor
    {
        private readonly OpenZWaveNetwork _network;

        public OpenZWaveNotificationProcessor(OpenZWaveNetwork network)
        {
            _network = network;
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

            //TODO: fill in other cases
            switch (notification.Type)
            {
                case NotificationType.AllNodesQueried:
                case NotificationType.AllNodesQueriedSomeDead:
                case NotificationType.AwakeNodesQueried:
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
                    device.Event = notification.Event;

                    //TODO: what else is this value used for?
                    if (device.Type.Equals(DeviceType.BinarySensor))
                    {
                        device.AddEvent(DeviceEvent.BinarySensorValueChanged(device, null));
                    }
                    break;

                case NotificationType.NodeNaming:
                case NotificationType.NodeNew:
                case NotificationType.NodeProtocolInfo:
                case NotificationType.NodeQueriesComplete:
                case NotificationType.NodeRemoved:
                    break;

                case NotificationType.Notification:
                    _network.SetReady();
                    break;

                case NotificationType.PollingDisabled:
                case NotificationType.PollingEnabled:
                case NotificationType.SceneEvent:
                    break;

                case NotificationType.ValueAdded:
                    notification.Device.Values.Add(notification.Value);
                    break;

                case NotificationType.ValueChanged:
                    notification.Device.ProcessValueChanged(notification.Value);
                    break;

                case NotificationType.ValueRefreshed:
                case NotificationType.ValueRemoved:
                    break;

                default:
                    throw new Exception("Unexpected notification type " + notification.Type);
            }
        }
    }
}
