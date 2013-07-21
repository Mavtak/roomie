using System;
using Roomie.CommandDefinitions.HomeAutomationCommands.Exceptions;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Exceptions;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    internal static class ZWaveDeviceExtensions
    {
        internal static TResult DoDeviceOperation<TResult>(this ZWaveDevice device, Func<TResult> operation)
        {
            var wasConnected = device.IsConnected;

            try
            {
                var result = operation();

                device.IsConnected = true;

                if (wasConnected != true)
                {
                    var @event = DeviceEvent.Found(device, null);
                    device.AddEvent(@event);
                }

                return result;
            }
            catch (ControlThink.ZWave.ZWaveException exception)
            {
                device.IsConnected = false;

                if (wasConnected == true)
                {
                    var @event = DeviceEvent.Lost(device, null);
                    device.AddEvent(@event);
                }

                if (exception is ControlThink.ZWave.DeviceNotRespondingException)
                {
                    throw new DeviceNotRespondingException(device, exception);
                }

                if (exception is ControlThink.ZWave.CommandTimeoutException)
                {
                    throw new CommandTimedOutException(device, exception);
                }

                throw new HomeAutomationException("Unexpected Z-Wave error: " + exception.Message, exception);
            }
        }

        internal static void DoDeviceOperation(this ZWaveDevice device, Action operation)
        {
            Func<int> wrappedOperation = () =>
            {
                operation();
                return 0;
            };

            device.DoDeviceOperation(wrappedOperation);
        }
    }
}
