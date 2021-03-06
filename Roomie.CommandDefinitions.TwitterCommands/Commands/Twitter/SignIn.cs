﻿using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.TwitterCommands.Commands.Twitter
{
    [StringParameter("Password")]
    [Description("This command saves the given Twitter users's password for use in other Twitter command calls.")]
    public class SignIn : TwitterCommand
    {
        protected override void Execute_TwitterDefinition(TwitterCommandContext context)
        {
            var interpreter = context.Interpreter;
            var users = context.TwitterUsers;

            string username = context.ReadParameter("Username").Value;
            string password = context.ReadParameter("Password").Value;

            foreach (string otherUsername in users.Keys)
            {
                if (otherUsername.Equals(username))
                    throw new RoomieRuntimeException("User " + username + " already signed in.");
            }

            try
            {
                var newUser = new Twitterizer.Framework.Twitter(username, password);
                users.Add(username, newUser);
            }
            catch (Twitterizer.Framework.TwitterizerException e)
            {
                throw new RoomieRuntimeException("Failed to sign in.  Recieved the following error: " + e.Message);
            }
        }

    }
}
