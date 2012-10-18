
using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.WebHookCommands
{
    public abstract class WebHookCommand : RoomieCommand
    {
        public WebHookCommand()
            : base()
        { }

        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var greaterContext = new WebhookCommandContext(context);

            Execute_WebHookCommand(greaterContext);
        }

        protected abstract void Execute_WebHookCommand(WebhookCommandContext context);
    }
}
