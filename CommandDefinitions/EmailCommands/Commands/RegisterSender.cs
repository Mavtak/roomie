using Roomie.Desktop.Engine;
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
            var scope = context.Scope;

            context.RegisterSender(
                host: scope.GetValue("Host").Value,
                port: scope.GetValue("Port").ToInteger(),
                enableSsl: scope.GetValue("UseSSL").ToBoolean(),
                username: scope.GetValue("Username").Value,
                password: scope.GetValue("Password").Value,
                senderAddress: scope.GetValue("Address").Value,
                senderName: scope.GetValue("Name").Value
            );
        }

    }
}
