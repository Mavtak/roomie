using System;
using Roomie.CommandDefinitions.HomeAutomationCommands.Exceptions;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Exceptions;
using Roomie.Desktop.Engine.WorkQueues;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    internal static class ZWaveDeviceExtensions
    {
        private const uint InitiallyDisconnectedRetries = 1;
        private const uint InitiallyConnectedRetries = 5;

        internal static TResult DoDeviceOperation<TResult>(this ZWaveDevice device, Func<TResult> operation)
        {
            var wasConnected = device.IsConnected;
            var network = (ZWaveNetwork) device.Network;
            var workQueue = network.WorkQueue;

            var retries = wasConnected == true ? InitiallyConnectedRetries : InitiallyDisconnectedRetries;

            var result = default(TResult);

            var work = new WorkQueueItem(() =>
                {
                    result = operation();
                }, retries, new[] {typeof (ControlThink.ZWave.ZWaveException)});
            workQueue.Add(work);

            var workResult = work.Result;

            if (workResult.Success)
            {
                device.IsConnected = true;

                if (wasConnected != true)
                {
                    var @event = DeviceEvent.Found(device, null);
                    device.AddEvent(@event);
                }

                return result;
            }

            device.IsConnected = false;

            if (wasConnected == true)
            {
                var @event = DeviceEvent.Lost(device, null);
                device.AddEvent(@event);
            }

            var exception = workResult.Exception;

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
