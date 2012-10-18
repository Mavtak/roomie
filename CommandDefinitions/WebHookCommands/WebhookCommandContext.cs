using System.Collections.Generic;

using Roomie.Desktop.Engine;

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
