using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.CommandDefinitions.EmailCommands.Commands
{
    [Group("Email")]
    [StringParameter("Host")]
    [IntegerParameter("Port")]
    [BooleanParameter("UseSSL")]
    [StringParameter("Username")]
    [StringParameter("Password")]
    [StringParameter("DisplayName")]
    [Description("Saves email information for later calls to Email.Send")]
    public class RegisterSender : EmailCommand
    {
        protected override void Execute_EmailDefinition(EmailCommandContext context)
        {
            context.RegisterSender(
                host: context.ReadParameter("Host").Value,
                port: context.ReadParameter("Port").ToInteger(),
                enableSsl: context.ReadParameter("UseSSL").ToBoolean(),
                username: context.ReadParameter("Username").Value,
                password: context.ReadParameter("Password").Value,
                senderAddress: context.ReadParameter("Address").Value,
                senderName: context.ReadParameter("Name").Value
            );
        }

    }
}
