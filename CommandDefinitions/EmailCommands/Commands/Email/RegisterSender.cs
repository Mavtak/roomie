

using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;


namespace Roomie.CommandDefinitions.EmailCommands.Commands.Email
{
    [StringParameter("Host")]
    [Parameter("Port", IntegerParameterType.Key)]
    [Parameter("UseSSL", BooleanParameterType.Key)]
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
