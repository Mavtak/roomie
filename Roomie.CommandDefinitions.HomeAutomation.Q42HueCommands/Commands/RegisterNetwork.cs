﻿using System;
using System.Linq;
using Q42.HueApi;
using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Common.HomeAutomation.Exceptions;
using Roomie.Desktop.Engine.Commands;

namespace Q42HueCommands.Commands
{
    [StringParameter("IP", "")]
    [Description("This command attempts to connect to a USB Z-Wave adapater.")]
    [Group("Q42Hue")]
    public class RegisterNetwork : HomeAutomationCommand
    {
        protected override void Execute_HomeAutomationDefinition(HomeAutomationCommandContext context)
        {
            var interpreter = context.Interpreter;
            var networks = context.Networks;
            var streamStore = context.StreamStore;
            var ip = context.ReadParameter("IP").Value;

            var appDataRepository = new AppDataRepository(streamStore);
            var appData = appDataRepository.Load();

            if (string.IsNullOrEmpty(ip))
            {
                interpreter.WriteEvent("Searching for Hue bridge...");

                ip = FindIp();
                interpreter.WriteEvent("Found " + ip);
            }

            var networkContext = new HomeAutomationNetworkContext(context.Engine, context.ThreadPool);
            var network = new Q42HueNetwork(networkContext, ip, appData, () => interpreter.WriteEvent("Press the link button on the Hue bridge."));
            networks.Add(network);

            appDataRepository.Save(appData);

            interpreter.WriteEvent("Done.");
        }

        private static string FindIp()
        {
            var locator = new HttpBridgeLocator();
            var ips = locator.LocateBridgesAsync(TimeSpan.FromSeconds(5)).Result.ToArray();

            if (ips.Length == 0)
            {
                throw new HomeAutomationException("No Hue bridge was found");
            }

            if (ips.Length > 1)
            {
                throw new HomeAutomationException("Multiple Hue bridges found.  One must be explicitly specified.");
            }

            var result = ips.First();

            return result;
        }
    }
}
