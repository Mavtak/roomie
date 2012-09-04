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
    internal class UpdateZWaveDeviceInfo : ActionHandler
    {
        public UpdateZWaveDeviceInfo()
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
            if (!request.Values.ContainsKey("DeviceName"))
            {
                response.ErrorMessage = "Device Name not set.";
                return;
            }
            if (!request.Values.ContainsKey("DeviceType"))
            {
                response.ErrorMessage = "Device Type not set.";
                return;
            }

            //read input
            uint homeId;
            byte nodeId;
            string deviceName;
            ZWaveDeviceType deviceType;
            try
            {
                homeId = Convert.ToUInt32(request.Values["HomeID"]);
                nodeId = Convert.ToByte(request.Values["NodeID"]);
                deviceName = request.Values["DeviceName"];
                deviceType = ZWaveDeviceType.GetTypeFromString(request.Values["DeviceType"]);
            }
            catch
            {
                response.ErrorMessage = "Error parsing input.";
                return;
            }

            ComputerRecord computer = callingComputer;
            UserEntry user = callingComputer.User;
            ZWaveNetworkRecord networkRecord = ZWaveManager.GetNetwork(user, homeId);

            ZWaveManager.RegisterZWaveDevice(networkRecord, nodeId, deviceName, deviceType);

            response.Values.Add("Response", "Device added. HomeID=" + homeId + ", NodeID=" + nodeId + ", Name=\"" + deviceName + "\" Type=\"" + deviceType + "\"");
        }
    }
}