using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.EmailCommands.Commands.Email
{
    [StringParameter("Password")]
    [StringParameter("DisplayName")]
    [Description("This is a shortcut command for Email.RegisterSender where the user is a Gmail account.")]
    public class RegisterGmailSender : EmailCommand
    {
        protected override void Execute_EmailDefinition(EmailCommandContext context)
        {
            var scope = context.Scope;

            context.RegisterSender(
                host: "smtp.gmail.com",
                port: 587,
                enableSsl: true,
                username: scope.GetValue("Address"),
                password: scope.GetValue("Password"),
                senderAddress: scope.GetValue("Address"),
                senderName: scope.GetValue("DisplayName")
            );
        }
    }
}
