using Roomie.Common.Exceptions;
using Roomie.Common.HomeAutomation;
using WebCommunicator;


namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    public class SyncWithCloud : HomeAutomationNetworkCommand
    {
        private Message buildRequest(Network network)
        {
            var message = new Message();
            message.Values.Add("Action", "SyncHomeAutomationNetwork");
            message.Values.Add("NetworkAddress", network.Name);

            foreach (var device in network.Devices)
            {
                message.Payload.Add(device.ToXElement());
            }

            return message;
        }

        protected override void Execute_HomeAutomationNetworkDefinition(HomeAutomationCommandContext context)
        {
            var interpreter = context.Interpreter;
            var network = context.Network;

            interpreter.WriteEvent("Syncing Home Automation Devices with the cloud...");

            Message request = buildRequest(network);

            Message response = WebHookConnector.SendMessage(context, request);

            //TODO: use LINQ?
            foreach (var deviceElement in response.Payload)
            {
                if (deviceElement.Name.LocalName != "HomeAutomationDevice")
                {
                    throw new RoomieRuntimeException("Received unexpected data while gettings tasks.  XML node named \"" + deviceElement.Name + "\"");
                }

                string networkAddress = deviceElement.Attribute("Address").Value;
                
                if (network.Devices.Contains(networkAddress))
                {
                    var device = (Device)network.Devices[networkAddress];

                    if (deviceElement.Attribute("Name") != null)
                    {
                        device.Name = deviceElement.Attribute("Name").Value;
                    }
                    //TODO: improve this
                    if (deviceElement.Attribute("Location") != null)
                        device.Location.Name = deviceElement.Attribute("Location").Value;

                    //TODO: type checking
                    if(deviceElement.Attribute("Type") != null)
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
