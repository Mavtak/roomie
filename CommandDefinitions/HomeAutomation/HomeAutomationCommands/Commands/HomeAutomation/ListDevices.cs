using System;
using Roomie.CommandDefinitions.HomeAutomationCommands.Attributes;
using Roomie.Common.HomeAutomation;
using Roomie.Common.TextUtilities;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    [BooleanParameter("Poll", false)]
    public class ListDevices : HomeAutomationNetworkCommand
    {
        protected override void Execute_HomeAutomationNetworkDefinition(HomeAutomationCommandContext context)
        {
            var interpreter = context.Interpreter;
            var network = context.Network;
            var scope = context.Scope;

            var poll = scope.GetValue("Poll").ToBoolean();

            //TODO: list all devices on all networks.
            //TODO: add type

            const string addressLabel = "Address";
            const string typeLabel = "Type";
            const string connectedLabel = "Connected";
            const string statusLabel = "Status";

            var addressLength = addressLabel.Length;
            var typeLength = typeLabel.Length;
            var connectedLength = connectedLabel.Length;
            var statusLength = statusLabel.Length;

            foreach (var device in network.Devices)
            {
                addressLength = Math.Max(addressLength, device.BuildVirtualAddress(false, false).Length);
                typeLength = Math.Max(typeLength, device.Type.Name.Length);
                statusLength = Math.Max(statusLength, device.Describe().Length);
            }

            var tableBuilder = new TextTable(new[] { addressLength, typeLength, connectedLength, statusLength });

            interpreter.WriteEvent(tableBuilder.StartOfTable(addressLabel, typeLabel, connectedLabel, statusLabel));

            foreach (var device in network.Devices)
            {
                var address = device.BuildVirtualAddress(false, false);

                if (poll)
                {
                    try
                    {
                        device.Poll();
                    }
                    catch
                    { }
                }

                var status = device.Describe();

                var connected = (device.IsConnected == true)?"Yes":" - ";

                interpreter.WriteEvent(tableBuilder.ContentLine(address, device.Type, connected, status));
            }

            interpreter.WriteEvent(tableBuilder.EndOfTable());
        }
    }
}
