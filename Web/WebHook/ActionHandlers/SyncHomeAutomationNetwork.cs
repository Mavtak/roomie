using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories;
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
            var computer = context.Computer;
            var user = context.User;
            var responseText = new StringBuilder();

            var computerRepository = context.RepositoryFactory.GetComputerRepository();
            var deviceRepository = context.RepositoryFactory.GetDeviceRepository();
            var networkRepository = context.RepositoryFactory.GetNetworkRepository();

            if (!request.Values.ContainsKey("NetworkAddress"))
            {
                response.ErrorMessage = "NetworkAddress not set";
                return;
            }
            var networkAddress = request.Values["NetworkAddress"];

            var network = networkRepository.Get(user, networkAddress);

            if (network == null)
            {
                //responseText.Append("Adding network '" + networkAddress + "'");
                network = Network.Create(networkAddress, user, networkAddress);
                networkRepository.Add(network);
            }

            computer.UpdatePing();
            computerRepository.Update(computer);

            network.UpdatePing(computer);
            networkRepository.Update(network);

            //responseText.Append("network: " + network);

            var sentDevices = ProcessSentDevices(request, response, user, network).ToList();

            var existingDevices = deviceRepository.Get(network);

            UpdateDevices(sentDevices, existingDevices, network, deviceRepository);

            var devicesToRemove = GetRemovedDevices(sentDevices, existingDevices);

            RemoveDevices(devicesToRemove, deviceRepository);

            AddDevicesToResponse(response, existingDevices);
            
            response.Values.Add("Response", responseText.ToString());
        }

        private static IEnumerable<IDeviceState> ProcessSentDevices(Message request, Message response, User user,  Network network)
        {
            var sentDevices = request.Payload.Select(x => x.ToDeviceState());
            sentDevices = sentDevices.Select(x => x.NewWithNetwork(network));

            return sentDevices;
        }

        private static void UpdateDevices(IEnumerable<IDeviceState> sentDevices, IEnumerable<Device> registeredDevices, Network network, IDeviceRepository deviceRepository)
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

                    deviceRepository.Add(newDevice);
                }
                else
                {
                    deviceRepository.Update(registeredDevice.Id, sentDevice);
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

        private static void RemoveDevices(IEnumerable<Device> devicesToRemove, IDeviceRepository deviceRepository)
        {
            foreach (var device in devicesToRemove.ToArray())
            {
                deviceRepository.Remove(device);
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
