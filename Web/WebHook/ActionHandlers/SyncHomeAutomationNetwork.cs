using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Models;
using WebCommunicator;

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

            var network = database.Networks.Get(user, networkAddress);

            if (network == null)
            {
                //responseText.Append("Adding network '" + networkAddress + "'");
                network = new EntityFrameworkNetworkModel(networkAddress)
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

            var sentDevices = ProcessSentDevices(request, response, user, network, database).ToList();

            var registeredDevices = new List<EntityFrameworkDeviceModel>(network.Devices);

            UpdateDevices(sentDevices, network, database);

            var devicesToRemove = GetRemovedDevices(sentDevices, registeredDevices);

            RemoveDevices(devicesToRemove, registeredDevices, database);

            AddDevicesToResponse(response, registeredDevices);
            
            response.Values.Add("Response", responseText.ToString());

            database.SaveChanges();
        }

        private static IEnumerable<IDeviceState> ProcessSentDevices(Message request, Message response, EntityFrameworkUserModel user,  EntityFrameworkNetworkModel network, IRoomieDatabaseContext database)
        {
            var sentDevices = request.Payload.Select(x => x.ToDeviceState());
            sentDevices = sentDevices.Select(x => x.NewWithNetwork(network));
            sentDevices = sentDevices.Select(x =>
                {
                    if (x.Location == null)
                    {
                        return x;
                    }

                    var existingLocationModel = database.GetDeviceLocation(user, x.Location.Format());

                    return x.NewWithLocation(existingLocationModel);
                });

            return sentDevices;
        }

        private static void UpdateDevices(IEnumerable<IDeviceState> sentDevices, EntityFrameworkNetworkModel network, IRoomieDatabaseContext database)
        {
            // go through the devices from the client and update the entries in the database
            foreach (var sentDevice in sentDevices)
            {
                var registeredDevice = network.Devices.FirstOrDefault(x => x.Address == sentDevice.Address && x.Network.Equals(sentDevice.NetworkState));

                if (registeredDevice == null)
                {
                    var newDevice = new EntityFrameworkDeviceModel
                    {
                        Address = sentDevice.Address,
                        IsConnected = sentDevice.IsConnected,
                        Name = sentDevice.Name,
                        Network = sentDevice.NetworkState as EntityFrameworkNetworkModel,
                        Location = sentDevice.Location as DeviceLocationModel,
                        Type = sentDevice.Type
                    };

                    newDevice.Update(sentDevice);

                    database.Devices.Add(newDevice);
                }
                else
                {
                    database.Devices.Update(registeredDevice.Id, sentDevice);
                }
            }
        }

        private static IEnumerable<EntityFrameworkDeviceModel> GetRemovedDevices(IEnumerable<IDeviceState> sentDevices, IEnumerable<EntityFrameworkDeviceModel> registeredDevices)
        {
            foreach (var registeredDevice in registeredDevices)
            {
                if (!sentDevices.Any(d => d.Address == registeredDevice.Address))
                {
                    yield return registeredDevice;
                }
            }
        }

        private static void RemoveDevices(IEnumerable<EntityFrameworkDeviceModel> devicesToRemove, List<EntityFrameworkDeviceModel> registeredDevices, IRoomieDatabaseContext database)
        {
            foreach (var device in devicesToRemove.ToArray())
            {
                database.Devices.Remove(device);
                registeredDevices.Remove(device);
            }
        }

        private static void AddDevicesToResponse(Message response, IEnumerable<IDeviceState> devices)
        {
            foreach (var device in devices)
            {
                response.Payload.Add(device.ToXElement());
            }
        }
    }
}
