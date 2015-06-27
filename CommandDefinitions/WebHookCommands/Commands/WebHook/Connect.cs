using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.WebHookCommands.Commands.WebHook
{
    [StringParameter("ComputerName")]
    [StringParameter("CommunicationURL")]
    [StringParameter("AccessKey")]
    [StringParameter("EncryptionKey")]
    [Description("This command creates a connection to a Roomie WebHook server.")]
    public class Connect : WebHookCommand
    {
        protected override void Execute_WebHookCommand(WebhookCommandContext context)
        {
            var engine = context.Engine;
            var interpreter = context.Interpreter;

            string computerName = context.ReadParameter("ComputerName").Value;
            string communicationUrl = context.ReadParameter("CommunicationURL").Value;
            string accessKey = context.ReadParameter("AccessKey").Value;
            string encryptionKey = context.ReadParameter("EncryptionKey").Value;

            var webhookEngine = new WebHookEngine(engine, computerName, communicationUrl, accessKey, encryptionKey);

            webhookEngine.RunAsync();

            var webhookEngines = context.WebhookEngines;

            webhookEngines.Add(communicationUrl, webhookEngine);
        }
    }
}
