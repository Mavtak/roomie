using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.EmailCommands
{
    [StringParameter("Address")]
    public abstract class EmailCommand : RoomieCommand
    {
        public EmailCommand()
            : base()
        { }

        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var dataStore = context.DataStore;

            string senderAddress = context.ReadParameter("Address").Value;

            EmailCommandContext greaterContext = new EmailCommandContext(context);

            var mailers = greaterContext.Mailers;
            
            if (!mailers.ContainsKey(senderAddress) && !this.Name.Contains("Register"))
            {
                throw new RoomieRuntimeException("Sender address \"" + senderAddress + "\" not registered.");
            }
            else if(mailers.ContainsKey(senderAddress))
            {
                greaterContext.Sender = mailers[senderAddress];;
            }

            Execute_EmailDefinition(greaterContext);
        }

        protected abstract void Execute_EmailDefinition(EmailCommandContext context);
    }
}
