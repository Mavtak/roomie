using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.EmailCommands.Commands.Email
{
    [Parameter("Password", "String")]
    [Parameter("DisplayName", "String")]
    [Description("This is a shortcut command for Email.RegisterSender where the user is a GoDaddy account.")]
    public class RegisterGoDaddySender : EmailCommand
    {
        protected override void Execute_EmailDefinition(EmailCommandContext context)
        {
            var scope = context.Scope;

            context.RegisterSender(
                host: "smtpout.secureserver.net",
                port: 465,
                enableSsl: true,
                username: scope.GetValue("Address"),
                password: scope.GetValue("Password"),
                senderAddress: scope.GetValue("Address"),
                senderName: scope.GetValue("DisplayName")
            );
        }
    }
}
