using OpenZWaveDotNet;
using NotificationType = OpenZWaveDotNet.ZWNotification.Type;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWaveNotification
    {
        private readonly OpenZWaveNetwork _network;
        private readonly ZWNotification _notification;

        public OpenZWaveNotification(OpenZWaveNetwork network, ZWNotification notification)
        {
            _network = network;
            _notification = notification;
        }

        public uint HomeId
        {
            get
            {
                return _notification.GetHomeId();
            }
        }

        public byte NodeId
        {
            get
            {
                return _notification.GetNodeId();
            }
        }

        private OpenZWaveDevice _device;
        public OpenZWaveDevice Device
        {
            get
            {
                if (_device == null)
                {
                    _device = _network.GetDevice(NodeId);
                }

                return _device;
            }
        }

        public NotificationType Type
        {
            get
            {
                return _notification.GetType();
            }
        }

        private OpenZWaveDeviceValue _value;
        public OpenZWaveDeviceValue Value
        {
            get
            {
                if (_value == null)
                {
                    _value = new OpenZWaveDeviceValue(_network, _notification.GetValueID());
                }

                return _value;
            }
        }

        public byte Event
        {
            get
            {
                return _notification.GetEvent();
            }
        }
    }
}
