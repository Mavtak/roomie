using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.EmailCommands.Commands
{
    [Group("Email")]
    [StringParameter("Password")]
    [StringParameter("DisplayName")]
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
                username: scope.ReadParameter("Address").Value,
                password: scope.ReadParameter("Password").Value,
                senderAddress: scope.ReadParameter("Address").Value,
                senderName: scope.ReadParameter("DisplayName").Value
            );
        }
    }
}
