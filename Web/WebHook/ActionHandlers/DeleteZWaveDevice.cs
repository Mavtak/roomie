using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Roomie.WebService;
using Roomie.WebService.DataStructures;
using Roomie.WebService.Managers;
using SomewhatGeeky.DataStructures;

using WebCommunicator;

using System.Text;

namespace Roomie.WebService.WebHook.ActionHandlers
{
    internal class DeleteZWaveDevice : ActionHandler
    {
        public DeleteZWaveDevice()
        { }

        public override void ProcessMessage(ComputerRecord callingComputer, Message request, Message response)
        {
            //validate input
            if (!request.Values.ContainsKey("HomeID"))
            {
                response.ErrorMessage = "Home ID not set";
                return;
            }
            if (!request.Values.ContainsKey("NodeID"))
            {
                response.ErrorMessage = "Node ID not set";
                return;
            }

            //read input
            uint? homeId;
            byte nodeId;
            try
            {
                homeId = Convert.ToUInt32(request.Values["HomeID"]);
                nodeId = Convert.ToByte(request.Values["NodeID"]);
            }
            catch
            {
                response.ErrorMessage = "Error parsing input.";
                return;
            }

            UserEntry userRecord = callingComputer.User;
            ZWaveNetworkRecord networkRecord = ZWaveManager.GetNetwork(userRecord, homeId);

            ZWaveDeviceRecord record = ZWaveManager.GetDevice(networkRecord, nodeId);

            if(record == null)
            {
                response.ErrorMessage = "Could not find device to delete";
                return;
            }

            record.DeleteRecord();

            response.Values.Add("Response", "Device with HomeID=" + homeId + "and NodeID= " + nodeId + " deleted.");

        }
    }
}