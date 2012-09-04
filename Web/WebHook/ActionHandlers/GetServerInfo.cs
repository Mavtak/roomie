using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Xml;

using WebCommunicator;
using Roomie.Web.Models;

namespace Roomie.Web.WebHook.ActionHandlers
{
    internal class GetServerInfo : ActionHandler
    {
        public GetServerInfo()
        { }

        public override void ProcessMessage(WebHookContext context)
        {
            var response = context.Response;

            StringBuilder builder = new StringBuilder();

            builder.Append("Here's my info!");

            builder.Append("\r\n");
            builder.Append("OS Version: ");
            builder.Append(Environment.OSVersion.ToString());

            builder.Append("\r\n");
            builder.Append("Processors: ");
            builder.Append(Environment.ProcessorCount);

            builder.Append("\r\n");
            builder.Append("Machine Name: ");
            builder.Append(Environment.MachineName);

            builder.Append("\r\n");
            builder.Append(".NET Version: ");
            builder.Append(Environment.Version.ToString());

            //builder.Append("\r\n");
            //builder.Append("Memory used: ");
            //builder.Append(Environment.WorkingSet);

            builder.Append("\r\n");
            builder.Append("Uptime: ");
            builder.Append(new TimeSpan(0, 0, 0, 0, Environment.TickCount).ToString());

            response.Values.Add("Response", builder.ToString());
        }
    }
}
