using System;
using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.EmailCommands.Commands
{
    [Group("Email")]
    [StringParameter("To")]
    [StringParameter("Subject")]
    [StringParameter("Body")]
    [Description("This command sense an email")]
    public class Send : EmailCommand
    {
        protected override void Execute_EmailDefinition(EmailCommandContext context)
        {
            var interpreter = context.Interpreter;
            var scope = context.Scope;
            var sender = context.Sender;

            string to = scope.ReadParameter("To").Value;
            string subject = scope.ReadParameter("Subject").Value;
            string body = scope.ReadParameter("Body").Value;

            bool result = false;
            try
            {
                result = sender.SendMessage(to, subject, body);

            }
            catch (Exception e)
            {
                throw new RoomieRuntimeException("Could not send message: " + e.Message);
            }

            if (!result)
            {
                throw new RoomieRuntimeException("Failed to send.");
            }
            else
            {
                interpreter.WriteEvent("sent.");
            }
        }
    }
}
