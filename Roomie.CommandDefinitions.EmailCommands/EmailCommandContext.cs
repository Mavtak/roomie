using System;
using System.Collections.Generic;
using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.EmailCommands
{
    public class EmailCommandContext : RoomieCommandContext
    {
        public Mailer Sender { get; set; }
        public EmailCommandContext(RoomieCommandContext context)
            : base(context)
        { }

        public Dictionary<string, Mailer> Mailers
        {
            get
            {
                // Access the dictionary of mailers from the central data store
                var key = typeof(InternalLibraryVersion);
                var value = DataStore.GetAdd<Dictionary<string, Mailer>>(key);
                return value;
            }
        }
        public void RegisterSender(string host, int port, bool enableSsl, string username, string password, string senderAddress, string senderName)
        {
            try
            {
                var newMailer = new Mailer(
                    host: host,
                    port: port,
                    enableSsl: enableSsl,
                    username: username,
                    password: password
                )
                {
                    DefaultFromAddress = senderAddress,
                    DefaultFromName = senderName
                };

                Mailers.Add(newMailer.DefaultFromAddress, newMailer);
            }
            catch (Exception e)
            {
                throw new RoomieRuntimeException("Problem registering email settings: " + e.Message);
            }
        }
    }
}
