using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;

using WebCommunicator;
using Roomie.Web.Models;
using Roomie.Web.Helpers;

namespace Roomie.Web.WebHook.ActionHandlers
{
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
                network = new NetworkModel
                {
                    Address = networkAddress,
                    Owner = user
                };
                database.Networks.Add(network);
            }

            network.AttatchedComputer = computer;
            computer.UpdatePing();
            network.UpdatePing();
            database.SaveChanges();

            //responseText.Append("network: " + network);

            var sentDevices = new List<DeviceModel>();
            foreach (var sentDeviceNode in request.Payload)
            {
                if (sentDeviceNode == null)
                {
                    response.ErrorMessage = "WTF? sentDeviceNode is null.";
                    return;
                }
                var sentDevice = new DeviceModel(sentDeviceNode);
                sentDevice.Network = network;
                if (sentDevice.Location != null)
                {
                    var existingLocationModel = database.GetDeviceLocation(user, sentDevice.Location.Name);

                    sentDevice.Location = existingLocationModel;
                }
                sentDevices.Add(sentDevice);
                //responseText.Append("\nreceived device: " + sentDevice);
            }

            var registeredDevices = new List<DeviceModel>(network.Devices);

            // go through the devices from the client and update the entries in the database
            foreach (var sentDevice in sentDevices)
            {
                if (!registeredDevices.Contains(sentDevice))
                {
                    network.Devices.Add(sentDevice);
                    //responseText.Append("\nadded device to the cloud: " + sentDevice);
                }
                else
                {
                    var registeredDevice = network.Devices.First(d => d.Equals(sentDevice));
                    if (registeredDevice.Power != sentDevice.Power)
                        registeredDevice.Power = sentDevice.Power;
                    if (registeredDevice.IsConnected != sentDevice.IsConnected)
                        registeredDevice.IsConnected = sentDevice.IsConnected;
                }
            }

            // return the updated list of devices to the client
            var devicesToRemove = new List<DeviceModel>();
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
                database.Devices.Remove(device);
                //responseText.Append("\nRemoved device from the cloud: " + device);
            }

            response.Values.Add("Response", responseText.ToString());

            database.SaveChanges();
        }
    }
}
