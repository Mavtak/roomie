using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.EmailCommands.Commands
{
    [Group("Email")]
    [StringParameter("Password")]
    [StringParameter("DisplayName")]
    [Description("This is a shortcut command for Email.RegisterSender where the user is a Gmail account.")]
    public class RegisterGmailSender : EmailCommand
    {
        protected override void Execute_EmailDefinition(EmailCommandContext context)
        {
            context.RegisterSender(
                host: "smtp.gmail.com",
                port: 587,
                enableSsl: true,
                username: context.ReadParameter("Address").Value,
                password: context.ReadParameter("Password").Value,
                senderAddress: context.ReadParameter("Address").Value,
                senderName: context.ReadParameter("DisplayName").Value
            );
        }
    }
}
