using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;

using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Exceptions;
using WebCommunicator;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public static class WebhookZWaveCommands
    {
        //internal static void UpdateDeviceInfo(RoomieController roomieController, HomeAutomationDevice device)
        //{
        //    Message outMessage = new Message();
        //    outMessage.Values.Add("Action", "UpdateZWaveDeviceInfo");
        //    outMessage.Values.Add("NetworkName", device.Network.Name);
        //    outMessage.Values.Add("NetworkAddress", device.NetworkAddress);
        //    outMessage.Values.Add("DeviceName", device.Name);
        //    outMessage.Values.Add("DeviceType", device.Type.ToString());

        //    WebHookConnector.SendMessage(roomieController, outMessage);
        //}

        //internal static void DeleteDevice(RoomieController roomieController, string networkName, string nodeId)
        //{
        //    Message outMessage = new Message();
        //    outMessage.Values.Add("Action", "DeleteZWaveDevice");
        //    outMessage.Values.Add("NetworkName", homeId.ToString());
        //    outMessage.Values.Add("NetworkAddress", nodeId.ToString());

        //    WebHookConnector.SendMessage(roomieController, outMessage);
        //}

    }
}
