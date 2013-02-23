﻿using System;
using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.ComputerCommands.Commands.Computer
{
    [Parameter("MAC", StringParameterType.Key)]
    [Description("This command sends a Wake On Lan (WOL) packet to to the specified MAC address")]
    public class WakeComputer : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var scope = context.Scope;

            string macAddress = scope.GetValue("MAC");

            try
            {
                Common.WakeFunction(macAddress);
            }
            catch (Exception e)
            {
                throw new RoomieRuntimeException("Error sending Wake On LAN message: " + e.Message);
            }
        }
    }
}
