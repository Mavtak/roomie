﻿using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.TwitterCommands
{
    [StringParameter("Username")]
    public abstract class TwitterCommand : RoomieCommand
    {

        public TwitterCommand()
            : base()
        { }

        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var greaterContext = new TwitterCommandContext(context);
            var users = greaterContext.TwitterUsers;

            string username = context.ReadParameter("Username").Value;

            if (!users.ContainsKey(username) && this.Name != "SignIn") //TODO: decouple this
            {
                throw new RoomieRuntimeException("Twitter user @" + username + " is not signed in.");
            }

            Twitterizer.Framework.Twitter user = null;

            if (users.ContainsKey(username))
            {
                user = users[username];
            }

            Execute_TwitterDefinition(greaterContext);
        }

        protected abstract void Execute_TwitterDefinition(TwitterCommandContext context);

    }
}
