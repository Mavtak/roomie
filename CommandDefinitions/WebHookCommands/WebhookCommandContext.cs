using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Exceptions;
using WebCommunicator;

namespace Roomie.CommandDefinitions.WebHookCommands
{
    public class WebhookCommandContext : RoomieCommandContext
    {
        public WebhookCommandContext(RoomieCommandContext context)
            : base(context)
        {

        }

        public Dictionary<string, WebHookEngine> WebhookEngines
        {
            get
            {
                return Common.GetWebhookEngines(DataStore);
            }
        }
    }
}
