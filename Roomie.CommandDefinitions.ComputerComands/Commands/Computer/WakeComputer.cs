﻿using System;
using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.ComputerCommands.Commands.Computer
{
    //TODO: make MAC address type
    [StringParameter("MAC")]
    [Description("This command sends a Wake On Lan (WOL) packet to to the specified MAC address")]
    public class WakeComputer : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var macAddress = context.ReadParameter("MAC").Value;
            macAddress = macAddress.Replace("-", "");

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
