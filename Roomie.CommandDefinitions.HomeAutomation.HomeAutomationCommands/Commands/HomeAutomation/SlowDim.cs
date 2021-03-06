﻿using System;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    //TODO: Roll start and end power into PowerAttribute?
    [ByteParameter("StartPower")]
    [ByteParameter("EndPower")]
    [TimeSpanParameter("Duration")]
    public class SlowDim : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var device = context.Device;

            var startPower = context.ReadParameter("StartPower").ToInteger();
            var endPower = context.ReadParameter("EndPower").ToInteger();

            TimeSpan duration = context.ReadParameter("Duration").ToTimeSpan();

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
                device.MultilevelSwitch.SetPower(currentPower);
                System.Threading.Thread.Sleep(timeStep);
            }
            device.MultilevelSwitch.SetPower(endPower);
        }
    }
}
