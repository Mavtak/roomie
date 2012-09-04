
namespace Roomie.CommandDefinitions.WebHookCommands.Commands.WebHook
{
    class ShutDownTasks : WebHookCommand
    {
        protected override void Execute_WebHookCommand(WebhookCommandContext context)
        {
            var engine = context.Engine;
            var interpreter = context.Interpreter;
            var webhookEngines = context.WebhookEngines;

            foreach (var webhookEngine in webhookEngines.Values)
            {
                webhookEngine.Stop();
            }
        }
    }
}
