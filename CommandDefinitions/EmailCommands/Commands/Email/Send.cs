using System;
using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.EmailCommands.Commands.Email
{
    [Parameter("To", StringParameterType.Key)]
    [Parameter("Subject", StringParameterType.Key)]
    [Parameter("Body", StringParameterType.Key)]
    [Description("This command sense an email")]
    public class Send : EmailCommand
    {
        protected override void Execute_EmailDefinition(EmailCommandContext context)
        {
            var interpreter = context.Interpreter;
            var scope = context.Scope;
            var sender = context.Sender;

            string to = scope.GetValue("To");
            string subject = scope.GetValue("Subject");
            string body = scope.GetValue("Body");

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
