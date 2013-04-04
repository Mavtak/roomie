using System;
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
            Action operation = () =>
            {
                BackingObject.PowerOn();
                IsConnected = true;
            };

            DoDeviceOperation(operation);
        }
        public override void PowerOff()
        {
            Action operation = () =>
            {
                BackingObject.PowerOff();
                IsConnected = true;
            };

            DoDeviceOperation(operation);
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
                //BackingObject.Ping();
                power = BackingObject.Level;
            };

            DoDeviceOperation(operation);
        }

        private TResult DoDeviceOperation<TResult>(Func<TResult> operation)
        {
            try
            {
                var result = operation();
                IsConnected = true;

                return result;
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

        private void DoDeviceOperation(Action operation)
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
