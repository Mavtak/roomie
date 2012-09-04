using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;

using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Exceptions;
using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.EmailCommands
{
    [Parameter("Address", "String")]
    public abstract class EmailCommand : RoomieCommand
    {
        public EmailCommand()
            : base()
        { }

        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var engine = context.Engine;
            var dataStore = context.DataStore;
            var scope = context.Scope;

            string senderAddress = scope.GetValue("Address");

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
