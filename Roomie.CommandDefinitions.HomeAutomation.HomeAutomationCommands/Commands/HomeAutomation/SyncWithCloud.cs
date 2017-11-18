using System.Linq;
using System.Xml.Linq;
using Roomie.Common.Api.Models;
using Roomie.Common.Exceptions;
using Roomie.Common.HomeAutomation;
using WebCommunicator;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    public class SyncWithCloud : HomeAutomationNetworkCommand
    {
        private Request buildRequest(INetwork network, HomeAutomationCommandContext context)
        {
            var sentDevicesXml = network.Devices
                .Select(x => x.ToXElement())
                .Select(x => x.ToString())
                .ToArray();

            var request = new Request
            {
                Action = "SyncWholeNetwork",
                Parameters = new System.Collections.Generic.Dictionary<string, object>
                {
                    {"networkAddress", network.Address },
                    {"computerName", WebHookConnector.GetComputerName(context)},
                    {"devicesXml",  sentDevicesXml},
                },
            };            

            return request;
        }

        protected override void Execute_HomeAutomationNetworkDefinition(HomeAutomationCommandContext context)
        {
            var interpreter = context.Interpreter;
            var network = context.Network;

            interpreter.WriteEvent("Syncing Home Automation Devices with the cloud...");

            var request = buildRequest(network, context);
            var response = WebHookConnector.Send<string[]>(context, "network", request);

            //TODO: use LINQ?
            foreach (var deviceElement in response.Data.Select(x => XElement.Parse(x)))
            {
                if (deviceElement.Name.LocalName != "HomeAutomationDevice")
                {
                    throw new RoomieRuntimeException("Received unexpected data while gettings tasks.  XML node named \"" + deviceElement.Name + "\"");
                }

                string networkAddress = deviceElement.Attribute("Address").Value;

                if (network.Devices.ContainsAddress(networkAddress))
                {
                    var device = network.Devices.GetDevice(networkAddress);

                    if (deviceElement.Attribute("Name") != null)
                    {
                        device.Name = deviceElement.Attribute("Name").Value;
                    }
                    //TODO: improve this
                    if (deviceElement.Attribute("Location") != null)
                        device.Location.Update(deviceElement.Attribute("Location").Value);

                    //TODO: type checking
                    if (deviceElement.Attribute("Type") != null)
                        device.Type = DeviceType.GetTypeFromString(deviceElement.Attribute("Type").Value);
                }
                else
                {
                    interpreter.WriteEvent("**WebHook returned a device (network address=" + networkAddress + ") that does not exist.**");
                }
            }
            interpreter.WriteEvent("...done");
        }
    }
}
