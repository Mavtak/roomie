﻿using System;
using System.Linq;
using Q42.HueApi;
using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Common.HomeAutomation.Exceptions;
using Roomie.Desktop.Engine.Commands;

namespace Q42HueCommands.Commands.Q42Hue
{
    [StringParameter("IP", "")]
    [StringParameter("Secret")]
    [Description("This command attempts to connect to a USB Z-Wave adapater.")]
    public class RegisterNetwork : HomeAutomationCommand
    {
        protected override void Execute_HomeAutomationDefinition(HomeAutomationCommandContext context)
        {
            var interpreter = context.Interpreter;
            var networks = context.Networks;
            var scope = context.Scope;
            var ip = scope.GetValue("IP");
            var secret = scope.GetValue("Secret");

            if (string.IsNullOrEmpty(ip))
            {
                interpreter.WriteEvent("Searching for Hue bridge...");

                ip = FindIp();
                interpreter.WriteEvent("Found " + ip);
            }

            var network = new Q42HueNetwork(new HomeAutomationNetworkContext(context.Engine, context.ThreadPool), ip, secret);
            networks.Add(network);

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
