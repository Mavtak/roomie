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
            context.RegisterSender(
                host: "smtpout.secureserver.net",
                port: 465,
                enableSsl: true,
                username: context.ReadParameter("Address").Value,
                password: context.ReadParameter("Password").Value,
                senderAddress: context.ReadParameter("Address").Value,
                senderName: context.ReadParameter("DisplayName").Value
            );
        }
    }
}
