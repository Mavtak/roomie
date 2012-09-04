

using Roomie.Desktop.Engine.Commands;


namespace Roomie.CommandDefinitions.EmailCommands.Commands.Email
{
    [Parameter("Host", "String")]
    [Parameter("Port", "Integer")]
    [Parameter("UseSSL", "Boolean")]
    [Parameter("Username", "String")]
    [Parameter("Password", "String")]
    [Parameter("DisplayName", "String")]
    [Description("Saves email information for later calls to Email.Send")]
    public class RegisterSender : EmailCommand
    {
        protected override void Execute_EmailDefinition(EmailCommandContext context)
        {
            var scope = context.Scope;

            context.RegisterSender(
                host: scope.GetValue("Host"),
                port: scope.GetInteger("Port"),
                enableSsl: scope.GetBoolean("UseSSL"),
                username: scope.GetValue("Username"),
                password: scope.GetValue("Password"),
                senderAddress: scope.GetValue("Address"),
                senderName: scope.GetValue("Name")
            );
        }

    }
}
