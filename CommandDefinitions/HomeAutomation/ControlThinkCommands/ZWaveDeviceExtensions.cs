using System;
using Roomie.CommandDefinitions.HomeAutomationCommands.Exceptions;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Exceptions;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    internal static class ZWaveDeviceExtensions
    {
        private const int InitiallyDisconnectedRetries = 1;
        private const int InitiallyConnectedRetries = 5;

        internal static TResult DoDeviceOperation<TResult>(this ZWaveDevice device, Func<TResult> operation)
        {
            var wasConnected = device.IsConnected;
            var retries = wasConnected == true ? InitiallyConnectedRetries : InitiallyDisconnectedRetries;

            Func<TResult> attempt = () =>
                {
                    var result = operation();

                    device.IsConnected = true;

                    if (wasConnected != true)
                    {
                        var @event = DeviceEvent.Found(device, null);
                        device.AddEvent(@event);
                    }

                    return result;
                };

            Func<Exception, Exception> fail = (exception) =>
                {
                    device.IsConnected = false;

                    if (wasConnected == true)
                    {
                        var @event = DeviceEvent.Lost(device, null);
                        device.AddEvent(@event);
                    }

                    if (exception is ControlThink.ZWave.DeviceNotRespondingException)
                    {
                        return new DeviceNotRespondingException(device, exception);
                    }

                    if (exception is ControlThink.ZWave.CommandTimeoutException)
                    {
                        return new CommandTimedOutException(device, exception);
                    }

                    return new HomeAutomationException("Unexpected Z-Wave error: " + exception.Message, exception);
                };

            while (true)
            {
                try
                {
                    return attempt();
                }
                catch (ControlThink.ZWave.ZWaveException exception1)
                {
                    if (retries <= 0)
                    {
                        throw fail(exception1);
                    }

                    retries--;
                }
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
