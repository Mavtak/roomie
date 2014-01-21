﻿using System.Linq;
using Roomie.CommandDefinitions.HomeAutomationCommands.Attributes;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.Exceptions;
using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    [DeviceParameter]
    [RetriesParameter]
    public abstract class HomeAutomationSingleDeviceCommand : HomeAutomationNetworkCommand
    {
        protected override void Execute_HomeAutomationNetworkDefinition(HomeAutomationCommandContext context)
        {
            var interpreter = context.Interpreter;
            var scope = context.Scope;
            var network = context.Network;
            var networks = context.Networks;
            var retries = scope.GetValue(RetriesParameterAttribute.Key).ToInteger();

            Device device = null;
            if (scope.VariableDefinedInThisScope("Device"))
            {
                var allDevices = networks.SelectMany(x => x.Devices);
                var address = scope.GetValue("Device").ToVirtualAddress();
                device = allDevices.GetDevice(address);
            }

            var greaterContext = new HomeAutomationSingleDeviceContext(context)
            {
                Device = device
            };

            while (true)
            {
                try
                {
                    Execture_HomeAutomationSingleDeviceDefinition(greaterContext);
                    return;
                }
                catch (HomeAutomationException exception)
                {
                    if (retries == 0)
                    {
                        throw;
                    }

                    interpreter.WriteEvent("retrying: " + exception.Message);

                    if (retries > 0)
                    {
                        retries--;
                    }
                }
            }
        }

        protected abstract void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context);
    }
}
