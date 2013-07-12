using System;
using Roomie.CommandDefinitions.HomeAutomationCommands.Attributes;
using Roomie.Common.HomeAutomation;
using Roomie.Common.TextUtilities;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    [AutoConnectParameterAttribute]
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
            const string powerLabel = "Power";

            var addressLength = addressLabel.Length;
            var typeLength = typeLabel.Length;
            var connectedLength = connectedLabel.Length;
            var powerLength = powerLabel.Length;

            foreach (var device in network.Devices)
            {
                addressLength = Math.Max(addressLength, device.BuildVirtualAddress(false, false).Length);
                typeLength = Math.Max(typeLength, device.Type.Name.Length);
            }

            var tableBuilder = new TextTable(new[] { addressLength, typeLength, connectedLength, powerLength });

            interpreter.WriteEvent(tableBuilder.StartOfTable(addressLabel, typeLabel, connectedLabel, powerLabel));

            foreach (Device device in network.Devices)
            {
                var address = device.BuildVirtualAddress(false, false);

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


                if (!device.Type.CanControl && device.Type != DeviceType.MotionDetector)
                {
                    power = "n/a";
                }
                else if (device.DimmerSwitch.Power == null)
                {
                    power = "?";
                }
                else if (device.DimmerSwitch.Power == 0)
                {
                    power = "off";
                }
                else
                {
                    power = device.DimmerSwitch.Power.ToString();
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
