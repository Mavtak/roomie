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
                network = Network.Create(networkAddress, user, networkAddress);
                database.Networks.Add(network);
            }

            computer.UpdatePing();
            database.Computers.Update(computer);

            network.UpdatePing(computer);
            database.Networks.Update(network);

            database.SaveChanges();

            //responseText.Append("network: " + network);

            var sentDevices = ProcessSentDevices(request, response, user, network, database).ToList();

            var existingDevices = database.Devices.Get(network);

            UpdateDevices(sentDevices, existingDevices, network, database);

            var devicesToRemove = GetRemovedDevices(sentDevices, existingDevices);

            RemoveDevices(devicesToRemove, database);

            AddDevicesToResponse(response, existingDevices);
            
            response.Values.Add("Response", responseText.ToString());

            database.SaveChanges();
        }

        private static IEnumerable<IDeviceState> ProcessSentDevices(Message request, Message response, User user,  Network network, IRoomieDatabaseContext database)
        {
            var sentDevices = request.Payload.Select(x => x.ToDeviceState());
            sentDevices = sentDevices.Select(x => x.NewWithNetwork(network));

            return sentDevices;
        }

        private static void UpdateDevices(IEnumerable<IDeviceState> sentDevices, IEnumerable<Device> registeredDevices, Network network, IRoomieDatabaseContext database)
        {
            // go through the devices from the client and update the entries in the database
            foreach (var sentDevice in sentDevices)
            {
                var registeredDevice = registeredDevices.FirstOrDefault(x => x.Address == sentDevice.Address && x.Network.Address == sentDevice.NetworkState.Address);

                if (registeredDevice == null)
                {
                    var newDevice = Device.Create(
                        address: sentDevice.Address,
                        isConnected: sentDevice.IsConnected,
                        name: sentDevice.Name,
                        network: network,
                        location: sentDevice.Location,
                        type: sentDevice.Type
                    );

                    newDevice.Update(sentDevice);

                    database.Devices.Add(newDevice);
                }
                else
                {
                    database.Devices.Update(registeredDevice.Id, sentDevice);
                }
            }
        }

        private static IEnumerable<Device> GetRemovedDevices(IEnumerable<IDeviceState> sentDevices, IEnumerable<Device> registeredDevices)
        {
            foreach (var registeredDevice in registeredDevices)
            {
                if (!sentDevices.Any(d => d.Address == registeredDevice.Address))
                {
                    yield return registeredDevice;
                }
            }
        }

        private static void RemoveDevices(IEnumerable<Device> devicesToRemove, IRoomieDatabaseContext database)
        {
            foreach (var device in devicesToRemove.ToArray())
            {
                database.Devices.Remove(device);
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
