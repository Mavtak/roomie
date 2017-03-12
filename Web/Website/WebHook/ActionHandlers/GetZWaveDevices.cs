using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Xml;

using WebCommunicator;
using Roomie.Web.Models;
using Roomie.Web.Helpers;

namespace Roomie.Web.WebHook.ActionHandlers
{
    internal class GetZWaveDevices : ActionHandler
    {
        public GetZWaveDevices()
        { }

        public override void ProcessMessage(WebHookContext context, Message request, Message response)
        {
            if (!request.Values.ContainsKey("HomeID"))
            {
                response.ErrorMessage = "Home ID not set.";
                return;
            }

            //read input
            uint homeId;
            try
            {
                homeId = Convert.ToUInt32(request.Values["HomeID"]);
            }
            catch
            {
                response.ErrorMessage = "Error parsing input. (1283)";
                return;
            }

            var database = context.Database;
            var computer = context.Computer;
            var user = context.User;

            var network = (from n in database.HomeAutomationNetwork
                           where
                           //n.HomeId == homeId &&
                              n.Owner.Id == user.Id
                           select n).First();
                          
                //          database.ZWaveNetworks
                //.FirstOrDefault(n => n.HomeId == homeId && n.Owner.Id == user.Id);

            if (network == null)
            {
                response.ErrorMessage = "ZWave network with Home ID " + homeId + " not found.";
                return;
            }

            network.AttatchedComputer = computer;
            network.UpdatePing();

            StringBuilder builder = new StringBuilder();
            builder.Append("<DeviceList>");

            foreach (var device in network.Devices)
            {
                builder.Append("<Device Address=\"");
                builder.Append(device.Address);
                builder.Append("\" DeviceName=\"");
                builder.Append(device.Name);
                builder.Append("\" DeviceType=\"");
                builder.Append(device.Type);
                builder.Append("\" />");
            }

            try
            {
                builder.Append("</DeviceList>");
                response.Payload.Add(XmlUtilities.StringToXml(builder.ToString()));
            }
            catch (XmlException)
            {
                response.ErrorMessage = "Error building response XML. (8472)";
                return;
            }

            try
            {
                response.Values.Add("Response", network.Devices.Count + " devices sent");
            }
            catch
            {
                response.ErrorMessage = "Server error: exception parsing XML. (error code 19382)";
            }
        }
    }
}