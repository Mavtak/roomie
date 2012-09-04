using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;

using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Exceptions;
using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.TwitterCommands
{
    [Parameter("Username", "String")]
    public abstract class TwitterCommand : RoomieCommand
    {

        public TwitterCommand()
            : base()
        { }

        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var scope = context.Scope;

            var greaterContext = new TwitterCommandContext(context);
            var users = greaterContext.TwitterUsers;

            string username = scope.GetValue("Username");

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
