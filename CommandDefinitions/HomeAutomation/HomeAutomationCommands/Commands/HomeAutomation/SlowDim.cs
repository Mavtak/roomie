﻿using System;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    //TODO: Roll start and end power into PowerAttribute?
    [Parameter("StartPower", "Byte")]
    [Parameter("EndPower", "Byte")]
    [Parameter("Duration", "TimeSpan")]
    public class SlowDim : SingleDeviceControlCommand
    {
        protected override void Execute_HomeAutomationNetwork(HomeAutomationCommandContext context)
        {
            var scope = context.Scope;
            var device = context.Device;

            var startPower = scope.GetInteger("StartPower");
            var endPower = scope.GetInteger("EndPower");

            TimeSpan duration = scope.GetTimeSpan("Duration");

            if (startPower == endPower)
            {
                System.Threading.Thread.Sleep(duration);
                return;
            }

            int numSteps = Math.Abs(endPower - startPower);
            TimeSpan timeStep = new TimeSpan(0, 0, 0, 0, ((int)duration.TotalMilliseconds)/(numSteps-1));


            int powerStep = 1;
            if (endPower < startPower)
                powerStep = -1;

            for (var currentPower = startPower; currentPower != endPower; currentPower = (currentPower + powerStep))
            {
                Console.Write(currentPower);
                device.Power = currentPower;
                System.Threading.Thread.Sleep(timeStep);
            }
            device.Power = endPower;
        }
    }
}
