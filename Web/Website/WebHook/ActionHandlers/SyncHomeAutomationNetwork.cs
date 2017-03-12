using System.Linq;
using System.Text;
using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Models;
using WebCommunicator;

namespace Roomie.Web.Website.WebHook.ActionHandlers
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

            var sentDevices = request.Payload;

            var syncWholeNetwork = new Controllers.Api.Network.Actions.SyncWholeNetwork(computerRepository, deviceRepository, networkRepository);
            var existingDevices = syncWholeNetwork.Run(computer, user, network, sentDevices);

            AddDevicesToResponse(response, existingDevices);

            response.Values.Add("Response", responseText.ToString());
        }

        private static void AddDevicesToResponse(Message response, IDeviceState[] devices)
        {
            foreach (var device in devices)
            {
                response.Payload.Add(device.ToXElement());
            }
        }
    }
}
