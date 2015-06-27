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
                host: scope.ReadParameter("Host").Value,
                port: scope.ReadParameter("Port").ToInteger(),
                enableSsl: scope.ReadParameter("UseSSL").ToBoolean(),
                username: scope.ReadParameter("Username").Value,
                password: scope.ReadParameter("Password").Value,
                senderAddress: scope.ReadParameter("Address").Value,
                senderName: scope.ReadParameter("Name").Value
            );
        }

    }
}
