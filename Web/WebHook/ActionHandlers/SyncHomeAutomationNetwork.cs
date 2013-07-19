using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Database;

namespace Roomie.Web.WebHook.ActionHandlers
{
    //TODO: fix this awful class.  Better yet, replace it with a REST server.
    internal class SyncHomeAutomationNetwork : ActionHandler
    {
        public SyncHomeAutomationNetwork()
        { }

        public override void ProcessMessage(WebHookContext context)
        {
            var request = context.Request;
            var response = context.Response;
            var database = context.Database;
            var computer = context.Computer;
            var user = context.User;
            var responseText = new StringBuilder();

            if (!request.Values.ContainsKey("NetworkAddress"))
            {
                response.ErrorMessage = "NetworkAddress not set";
                return;
            }
            var networkAddress = request.Values["NetworkAddress"];

            var network = (from n in database.Networks //TODO: user.HomeAutomationNetworks.CreateQueryContext(or whatever)
                           where //TODO: fix 
                           n.Address == networkAddress
                           && n.Owner.Id == user.Id
                           select n).FirstOrDefault();

            if (network == null)
            {
                //responseText.Append("Adding network '" + networkAddress + "'");
                network = new NetworkModel(networkAddress)
                {
                    Owner = user
                };
                database.Networks.Add(network);
            }

            network.AttatchedComputer = computer;
            computer.UpdatePing();
            network.UpdatePing();
            database.SaveChanges();

            //responseText.Append("network: " + network);

            var sentDevices = new List<IDeviceState>();
            foreach (var sentDeviceNode in request.Payload)
            {
                if (sentDeviceNode == null)
                {
                    response.ErrorMessage = "WTF? sentDeviceNode is null.";
                    return;
                }
                var sentDevice = sentDeviceNode.ToDeviceState();
                
                //TODO: improve this
                sentDevice = sentDevice.NewWithNetwork(network);

                if (sentDevice.Location != null)
                {
                    var existingLocationModel = database.GetDeviceLocation(user, sentDevice.Location.Name);

                    sentDevice = sentDevice.NewWithLocation(existingLocationModel);
                }
                sentDevices.Add(sentDevice);
                //responseText.Append("\nreceived device: " + sentDevice);
            }

            var registeredDevices = new List<IDeviceState>(network.Devices);

            // go through the devices from the client and update the entries in the database
            foreach (var sentDevice in sentDevices)
            {
                if (!registeredDevices.Any(x => x.Address == sentDevice.Address && x.Network.Equals(sentDevice.Network)))
                {

                    var newDevice = new DeviceModel
                        {
                            Address = sentDevice.Address,
                            IsConnected = sentDevice.IsConnected,
                            MaxPower = sentDevice.DimmerSwitchState.MaxPower??0,
                            Name = sentDevice.Name,
                            Network = sentDevice.Network as NetworkModel,
                            Location = sentDevice.Location as DeviceLocationModel,
                            Power = sentDevice.DimmerSwitchState.Power,
                            Type = sentDevice.Type
                        };
                    network.Devices.Add(newDevice);
                    //responseText.Append("\nadded device to the cloud: " + sentDevice);
                }
                else
                {
                    var registeredDevice = network.Devices.First(x => x.Address == sentDevice.Address && x.Network.Equals(sentDevice.Network));
                    if (registeredDevice.Power != sentDevice.DimmerSwitchState.Power)
                        registeredDevice.Power = sentDevice.DimmerSwitchState.Power;
                    if (registeredDevice.IsConnected != sentDevice.IsConnected)
                        registeredDevice.IsConnected = sentDevice.IsConnected;

                    if (sentDevice.ThermostatState != null)
                    {
                        //hacks! :D  (but seriously, this is just lazy.)
                        registeredDevice.Notes = sentDevice.ThermostatState.ToXElement().ToString();
                    }
                }
            }

            // return the updated list of devices to the client
            var devicesToRemove = new List<IDeviceState>();
            foreach(var registeredDevice in registeredDevices)
            {
                if (sentDevices.Find(d => 
                    d.Address == 
                    registeredDevice.Address
                    ) == null)
                    devicesToRemove.Add(registeredDevice);
                else
                    response.Payload.Add(registeredDevice.ToXElement());
            }

            foreach (var device in devicesToRemove)
            {
                var deviceToRemove = database.Devices.First(x => x.Address == device.Address && x.Network.Equals(device.Network));
                database.Devices.Remove(deviceToRemove);
                //responseText.Append("\nRemoved device from the cloud: " + device);
            }

            response.Values.Add("Response", responseText.ToString());

            database.SaveChanges();
        }
    }
}
