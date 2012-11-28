using System;
using Roomie.CommandDefinitions.HomeAutomationCommands.Attributes;
using Roomie.Common.TextUtilities;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    [AutoConnectParameterAttribute]
    [Parameter("Poll", "Boolean", "False")]
    public class ListDevices : HomeAutomationNetworkCommand
    {
        protected override void Execute_HomeAutomationNetwork(HomeAutomationCommandContext context)
        {
            var interpreter = context.Interpreter;
            var network = context.Network;
            var scope = context.Scope;

            var poll = scope.GetBoolean("Poll");

            //TODO: list all devices on all networks.
            //TODO: add type

            int addressLength = 10;

            foreach (var device in network.Devices)
            {
                addressLength = Math.Max(addressLength, device.BuildVirtualAddress(true, false).Length);
            }

            var tableBuilder = new TextTable(new int[] { addressLength, 10, 9, 5 });

            addressLength += 2;

            interpreter.WriteEvent(tableBuilder.StartOfTable("Address", "Type", "Connected", "Power"));

            foreach (Device device in network.Devices)
            {
                var address = device.BuildVirtualAddress(true, false);

                string power = "";

                if (poll && device.Type.CanControl)
                {
                    try
                    {
                        device.Poll();
                    }
                    catch
                    { }
                }


                if (!device.Type.CanControl)
                {
                    power = "n/a";
                }
                else if (device.Power == null)
                {
                    power = "?";
                }
                else if (device.Power == 0)
                {
                    power = "off";
                }
                else
                {
                    power = device.Power.ToString();
                    if (device.IsConnected != true)
                    {
                        power = power + "?";
                    }
                }

                var connected = (device.IsConnected == true)?"Yes":" - ";

                interpreter.WriteEvent(tableBuilder.ContentLine(address, device.Type, connected, power));
            }

            interpreter.WriteEvent(tableBuilder.EndOfTable());
        }
    }
}
