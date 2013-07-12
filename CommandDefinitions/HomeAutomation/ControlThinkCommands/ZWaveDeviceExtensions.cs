using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roomie.CommandDefinitions.HomeAutomationCommands.Exceptions;
using Roomie.Common.HomeAutomation.Exceptions;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    public static class ZWaveDeviceExtensions
    {
        internal static TResult DoDeviceOperation<TResult>(this ZWaveDevice device, Func<TResult> operation)
        {
            try
            {
                var result = operation();
                device.IsConnected = true;

                return result;
            }
            catch (ControlThink.ZWave.ZWaveException exception)
            {
                device.IsConnected = false;

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
