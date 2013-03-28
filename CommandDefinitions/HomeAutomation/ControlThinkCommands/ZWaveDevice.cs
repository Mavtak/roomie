using Roomie.CommandDefinitions.HomeAutomationCommands.Exceptions;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.Exceptions;
using BaseDevice = Roomie.CommandDefinitions.HomeAutomationCommands.Device;
using BaseNetwork = Roomie.CommandDefinitions.HomeAutomationCommands.Network;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    public class ZWaveDevice : BaseDevice
    {
        public global::ControlThink.ZWave.Devices.ZWaveDevice BackingObject { get; private set; }

        public ZWaveDevice(BaseNetwork network, global::ControlThink.ZWave.Devices.ZWaveDevice backingDevice, DeviceType type = null, string name = null)
            : base(network, 99, type, name)
        {
            BackingObject = backingDevice;

            BackingObject.PollEnabled = false;

            address = backingDevice.NodeID.ToString();

            if (type != null && type.CanControl && !type.CanDim)
            {
                MaxPower = 255;
            }

            BackingObject.LevelChanged += (sender, args) =>
                {
                    power = args.Level;
                    IsConnected = true;
                    PowerChanged();
                };
        }

        public override void PowerOn()
        {
            try
            {
                BackingObject.PowerOn();
                IsConnected = true;
            }
            catch (ControlThink.ZWave.DeviceNotRespondingException exception)
            {
                IsConnected = false;
                throw new HomeAutomationTimeoutException(this, exception);
            }
            catch (ControlThink.ZWave.CommandTimeoutException exception)
            {
                IsConnected = false;
                throw new HomeAutomationTimeoutException(this, exception);
            }
            catch (ControlThink.ZWave.ZWaveException exception)
            {
                IsConnected = false;
                throw new HomeAutomationException("Unexpected Z-Wave error: " + exception.Message, exception);
            }
        }
        public override void PowerOff()
        {
            try
            {
                BackingObject.PowerOff();
                IsConnected = true;
            }
            catch (ControlThink.ZWave.DeviceNotRespondingException exception)
            {
                IsConnected = false;
                throw new HomeAutomationTimeoutException(this, exception);
            }
            catch (ControlThink.ZWave.CommandTimeoutException exception)
            {
                IsConnected = false;
                throw new HomeAutomationTimeoutException(this, exception);
            }
            catch (ControlThink.ZWave.ZWaveException exception)
            {
                IsConnected = false;
                throw new HomeAutomationException("Unexpected Z-Wave error: " + exception.Message, exception);
            }
        }

        protected override int? SetPower(int power)
        {
            try
            {
                BackingObject.Level = (byte)power;
                IsConnected = true;

                //TODO: should this method still return the power?
                return power;
            }
            catch (ControlThink.ZWave.DeviceNotRespondingException exception)
            {
                IsConnected = false;
                throw new HomeAutomationTimeoutException(this, exception);
            }
            catch (ControlThink.ZWave.CommandTimeoutException exception)
            {
                IsConnected = false;
                throw new HomeAutomationTimeoutException(this, exception);
            }
            catch (ControlThink.ZWave.ZWaveException exception)
            {
                IsConnected = false;
                throw new HomeAutomationException("Unexpected Z-Wave error: " + exception.Message, exception);
            }
        }

        public override void Poll()
        {
            try
            {
                //BackingObject.Ping();
                power = BackingObject.Level;
                IsConnected = true;

            }
            catch (ControlThink.ZWave.DeviceNotRespondingException exception)
            {
                IsConnected = false;
                throw new HomeAutomationTimeoutException(this, exception);
            }
            catch (ControlThink.ZWave.CommandTimeoutException exception)
            {
                IsConnected = false;
                throw new HomeAutomationTimeoutException(this, exception);
            }
            catch (ControlThink.ZWave.ZWaveException exception)
            {
                IsConnected = false;
                throw new HomeAutomationException("Unexpected Z-Wave error: " + exception.Message, exception);
            }
        }
    }
}
