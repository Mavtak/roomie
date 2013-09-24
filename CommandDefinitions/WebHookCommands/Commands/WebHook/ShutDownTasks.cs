
namespace Roomie.CommandDefinitions.WebHookCommands.Commands.WebHook
{
    class ShutDownTasks : WebHookCommand
    {
        protected override void Execute_WebHookCommand(WebhookCommandContext context)
        {
            var interpreter = context.Interpreter;
            var webhookEngines = context.WebhookEngines;

            interpreter.WriteEvent("Shutting down WebHook...");

            foreach (var webhookEngine in webhookEngines.Values)
            {
                webhookEngine.Stop();
            }

            interpreter.WriteEvent("Done shutting down WebHook");
        }
    }
}
