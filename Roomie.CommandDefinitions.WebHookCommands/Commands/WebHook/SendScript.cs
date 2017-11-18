using Roomie.Desktop.Engine.Commands;

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
                    webHookEngine.Send<object>("computer", new Roomie.Common.Api.Models.Request
                    {
                        Action = "RunScript",
                        Parameters = new System.Collections.Generic.Dictionary<string, object>
                        {
                            {"computerName", computerName },
                            {"script", innerCommands.OriginalText },
                            {"source", "Roomie Desktop" },
                        },
                    });
                }
            }
        }
    }
}
