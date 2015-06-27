using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;
using WebCommunicator;

namespace Roomie.CommandDefinitions.WebHookCommands.Commands.WebHook
{
    [StringParameter("ComputerName")]
    [Description("This command sends a script through the WebHook server to the specified computer.")]
    public class SendScript : WebHookCommand
    {
        protected override void Execute_WebHookCommand(WebhookCommandContext context)
        {
            var interpreter = context.Interpreter;
            var originalCommand = context.OriginalCommand;
            var innerCommands = originalCommand.InnerCommands;

            var webHookEngines = context.WebhookEngines;

            var computerName = context.ReadParameter("ComputerName").Value;

            var outMessage = new Message();

            //Dictionary<string, string> values = new Dictionary<string, string>();
            outMessage.Values.Add("Action", "SendScript");
            outMessage.Values.Add("TargetComputerName", computerName);
            outMessage.Values.Add("ScriptText", innerCommands.OriginalText);

            foreach (var webHookEngine in webHookEngines.Values)
            {
                if (webHookEngine.ComputerName.Equals(computerName))
                {
                    //TODO: improve?
                    interpreter.WriteEvent("hey, that's me!");
                    webHookEngine.AddCommands(innerCommands);
                }
                else
                {
                    webHookEngine.SendMessage(outMessage);
                }
            }
        }
    }
}
