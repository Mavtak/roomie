using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.EmailCommands.Commands.Email
{
    [Description("This command unregistered the given sender.")]
    public class RemoveSender : EmailCommand
    {
        protected override void Execute_EmailDefinition(EmailCommandContext context)
        {
            var mailers = context.Mailers;
            var sender = context.Sender;

            mailers.Remove(sender.DefaultFromAddress);
        }
    }
}
