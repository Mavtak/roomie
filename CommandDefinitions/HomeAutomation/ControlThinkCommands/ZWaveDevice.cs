using System;
using Roomie.CommandDefinitions.HomeAutomationCommands.Exceptions;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.Exceptions;
using BaseDevice = Roomie.CommandDefinitions.HomeAutomationCommands.Device;
using BaseNetwork = Roomie.CommandDefinitions.HomeAutomationCommands.Network;
using BackingDevice = ControlThink.ZWave.Devices.ZWaveDevice;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    public class ZWaveDevice : BaseDevice
    {
        internal BackingDevice BackingObject { get; private set; }

        public ZWaveDevice(BaseNetwork network, BackingDevice backingDevice, DeviceType type = null, string name = null)
            : base(network, 99, type, name)
        {
            BackingObject = backingDevice;

            BackingObject.PollEnabled = false;

            Address = backingDevice.NodeID.ToString();

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

        public override IToggleSwitch ToggleSwitch
        {
            get
            {
                return new ZWaveToggleSwitch(this);
            }
        }

        protected override int? SetPower(int power)
        {
            Func<int?> operation = () =>
            {
                BackingObject.Level = (byte)power;

                //TODO: should this method still return the power?
                return power;
            };

            var result = DoDeviceOperation(operation);

            return result;
        }

        public override void Poll()
        {
            Action operation = () =>
            {
                power = BackingObject.Level;
            };

            DoDeviceOperation(operation);
        }

        internal TResult DoDeviceOperation<TResult>(Func<TResult> operation)
        {
            try
            {
                var result = operation();
                IsConnected = true;

                return result;
            }
            catch (ControlThink.ZWave.ZWaveException exception)
            {
                IsConnected = false;

                if (exception is ControlThink.ZWave.DeviceNotRespondingException)
                {
                    throw new DeviceNotRespondingException(this, exception);
                }

                if (exception is ControlThink.ZWave.CommandTimeoutException)
                {
                    throw new CommandTimedOutException(this, exception);
                }

                throw new HomeAutomationException("Unexpected Z-Wave error: " + exception.Message, exception);
            }
        }

        internal void DoDeviceOperation(Action operation)
        {
            Func<int> wrappedOperation = () =>
                {
                    operation();
                    return 0;
                };

            DoDeviceOperation(wrappedOperation);
        }
    }
}
