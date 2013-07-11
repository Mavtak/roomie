using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.EmailCommands.Commands.Email
{
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
            var scope = context.Scope;

            context.RegisterSender(
                host: scope.GetValue("Host"),
                port: scope.GetValue("Port").ToInteger(),
                enableSsl: scope.GetValue("UseSSL").ToBoolean(),
                username: scope.GetValue("Username"),
                password: scope.GetValue("Password"),
                senderAddress: scope.GetValue("Address"),
                senderName: scope.GetValue("Name")
            );
        }

    }
}
