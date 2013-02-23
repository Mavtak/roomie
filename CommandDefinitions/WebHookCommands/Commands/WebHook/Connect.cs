using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.WebHookCommands.Commands.WebHook
{
    [Parameter("ComputerName", StringParameterType.Key)]
    [Parameter("CommunicationURL", StringParameterType.Key)]
    [Parameter("AccessKey", StringParameterType.Key)]
    [Parameter("EncryptionKey", StringParameterType.Key)]
    [Description("This command creates a connection to a Roomie WebHook server.")]
    public class Connect : WebHookCommand
    {
        protected override void Execute_WebHookCommand(WebhookCommandContext context)
        {
            var engine = context.Engine;
            var interpreter = context.Interpreter;
            var scope = context.Scope;

            string computerName = scope.GetValue("ComputerName");
            string communicationUrl = scope.GetValue("CommunicationURL");
            string accessKey = scope.GetValue("AccessKey");
            string encryptionKey = scope.GetValue("EncryptionKey");

            var webhookEngine = new WebHookEngine(engine, computerName, communicationUrl, accessKey, encryptionKey);

            webhookEngine.RunAsync();

            var webhookEngines = context.WebhookEngines;

            webhookEngines.Add(communicationUrl, webhookEngine);
        }
    }
}
