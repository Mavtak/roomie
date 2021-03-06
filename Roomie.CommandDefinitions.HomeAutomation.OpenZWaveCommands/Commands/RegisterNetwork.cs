﻿using System;
using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.Commands
{
    [StringParameter("Port")]
    [Description("This command attempts to connect to a USB Z-Wave adapater.")]
    [Group("OpenZWave")]
    public class RegisterNetwork : HomeAutomationCommand
    {
        protected override void Execute_HomeAutomationDefinition(HomeAutomationCommandContext context)
        {
            var interpreter = context.Interpreter;
            var networks = context.Networks;
            var port = context.ReadParameter("Port").Value;

            if (port.StartsWith("COM", StringComparison.InvariantCultureIgnoreCase))
            {
              port = @"\\.\" + port;
            }

            interpreter.WriteEvent("Searching for Z-Wave network adapater at " + port + "...");

            var network = new OpenZWaveNetwork(new HomeAutomationNetworkContext(context.Engine, context.ThreadPool), port);
            networks.Add(network);

            interpreter.WriteEvent("Done.");
        }
    }
}
