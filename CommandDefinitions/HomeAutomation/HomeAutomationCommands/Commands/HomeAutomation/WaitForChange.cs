using System;
using Roomie.Common.Exceptions;
using Roomie.Common.HomeAutomation.Exceptions;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    [Parameter("PollInterval", "TimeSpan", "1 Second")]
    [Parameter("MaxErrors", "Integer", "5")]
    public class WaitForChange : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var interpreter = context.Interpreter;
            var scope = context.Scope;
            var device = context.Device;

            TimeSpan pollInterval = scope.GetTimeSpan("PollInterval");
            int maxErrors = scope.GetInteger("MaxErrors");

            if (pollInterval.Seconds < 0)
                throw new RoomieRuntimeException("PollInterval must be a positive (or 0) time interval.");
            if (maxErrors < -1)
                throw new RoomieRuntimeException("MaxErrors must be a positive (or 0, -1) integer.");

            device.Poll();

            var startPower = device.Power;
            var currentPower = startPower;

            int numFails = 0;
            while (true)
            {
                System.Threading.Thread.Sleep(pollInterval);
                try
                {
                    device.Poll();

                    currentPower = device.Power;
                    if (currentPower != startPower)
                        return;
                }
                catch(HomeAutomationException e) //TODO: specialized timeout exception?
                {
                    interpreter.WriteEvent("fail");
                    numFails++;
                    if (maxErrors>=0
                        && numFails >= maxErrors)
                        throw new RoomieRuntimeException("HomeAutomation Error: " + e.Message);
                }
            }

            //WaitForChangeHelper helper = new WaitForChangeHelper(interpreter);
            //zWaveDevice.LevelChanged += new ControlThink.ZWave.Devices.ZWaveDevice.LevelChangedEventHandler(helper.zWaveDevice_LevelChanged);
            //helper.WaitUntilChange();
        }

        
    }
}
